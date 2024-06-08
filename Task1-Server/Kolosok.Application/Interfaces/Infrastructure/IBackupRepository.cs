namespace Kolosok.Application.Interfaces.Infrastructure;

public interface IBackupRepository
{
    Task<(string fileName, byte[] file)> CreateBackupAsync();
    public Task RestoreBackupAsync(Stream backupStream);
}