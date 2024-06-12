using System.Diagnostics;
using Kolosok.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Kolosok.Infrastructure.Repositories;

public class BackupRepository : IBackupRepository
{
    private readonly IConfiguration _configuration;
    
    public BackupRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<(string fileName, byte[] file)> CreateBackupAsync()
    {
        var connectionString = _configuration.GetConnectionString("KolosokConnectionString");

        var databaseName = connectionString.Split(";").First(x => x.Contains("Database")).Split("=").Last();
        var userId = connectionString.Split(";").First(x => x.Contains("Username")).Split("=").Last();
        var password = connectionString.Split(";").First(x => x.Contains("Password")).Split("=").Last();
        var host = connectionString.Split(";").First(x => x.Contains("Host")).Split("=").Last();
        var port = connectionString.Split(";").First(x => x.Contains("Port")).Split("=").Last();

        var backupPath = _configuration["BackupPath"];
        var backupFileName = $"{databaseName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sql";
        var restoreFilePath = Path.GetTempPath();
        var backupFilePath = Path.Combine(restoreFilePath, backupFileName);

        var containerName = _configuration["ConnectionStrings:ContainerName"];

        using var process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/C docker exec -i {containerName} pg_dump --dbname={databaseName} --username={userId} --host={host} --port={port} --format=plain --no-password --format=custom > {backupFilePath}";
        process.StartInfo.Environment["PGPASSWORD"] = password;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync();
            throw new Exception($"pg_dump failed: {error}");
        }

        await using var fileStream = new FileStream(backupFilePath, FileMode.Open, FileAccess.Read);
        using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        fileStream.Close();
        File.Delete(backupFilePath);
        var file = memoryStream.ToArray();

        return (backupFileName, file);
    }


    public async Task RestoreBackupAsync(Stream backupStream)
    {
        var connectionString = _configuration.GetConnectionString("KolosokConnectionString");

        var databaseName = connectionString.Split(";").First(x => x.Contains("Database")).Split("=").Last();
        var userId = connectionString.Split(";").First(x => x.Contains("Username")).Split("=").Last();
        var password = connectionString.Split(";").First(x => x.Contains("Password")).Split("=").Last();
        var host = connectionString.Split(";").First(x => x.Contains("Host")).Split("=").Last();
        var port = connectionString.Split(";").First(x => x.Contains("Port")).Split("=").Last();

        var containerName = _configuration["ConnectionStrings:ContainerName"];
        var restoreFilePath = Path.GetTempFileName();

        try
        {
            using (var fileStream = new FileStream(restoreFilePath, FileMode.Create, FileAccess.Write))
            {
                await backupStream.CopyToAsync(fileStream);
            }

            using var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/C docker exec -i {containerName} pg_restore --dbname={databaseName} --username={userId} --host={host} --port={port} --no-password --clean --create -f {restoreFilePath}";
            process.StartInfo.Environment["PGPASSWORD"] = password;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;

            process.Start();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception($"pg_restore failed: {await process.StandardError.ReadToEndAsync()}");
            }
        }
        finally
        {
            if (File.Exists(restoreFilePath))
            {
                File.Delete(restoreFilePath);
            }
        }
    }
}