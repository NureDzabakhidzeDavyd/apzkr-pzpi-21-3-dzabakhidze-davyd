using Kolosok.Application.Contracts.Victim;

namespace Kolosok.Application.Contracts.Diagnosis;

public class DiagnosisResponse
{
    public string Name { get; set; }
    public string Note { get; set; }
    
    public DateTime DetectionTime { get; set; } = DateTime.Now;
    
    public Guid VictimId { get; set; }
    public VictimResponse Victim { get; set; }
}