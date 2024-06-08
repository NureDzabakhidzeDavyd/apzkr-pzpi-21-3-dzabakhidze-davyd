namespace Kolosok.Application.Features.Diagnoses.Commands;

public class UpdateDiagnosisRequestCommand
{
    public string Name { get; set; }
    public string Note { get; set; }
    
    public DateTime DetectionTime { get; set; } = DateTime.Now;
    
    public Guid VictimId { get; set; }
}