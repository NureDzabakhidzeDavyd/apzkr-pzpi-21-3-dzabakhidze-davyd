using Kolosok.Application.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class BackupController : ControllerBase
{
    private readonly IBackupRepository _backupRepository;

    public BackupController(IBackupRepository backupRepository)
    {
        _backupRepository = backupRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetBackup()
    {
        var file = await _backupRepository.CreateBackupAsync();
        return File(file.file, "application/octet-stream", file.fileName);
    }

    [HttpPost("restore")]
    public async Task<IActionResult> RestoreBackup(IFormFile backupFile)
    {
        if (backupFile == null || backupFile.Length == 0)
        {
            return BadRequest("Invalid backup file.");
        }

        await using var backupStream = backupFile.OpenReadStream();
        await _backupRepository.RestoreBackupAsync(backupStream);
        return Ok();
    }
}