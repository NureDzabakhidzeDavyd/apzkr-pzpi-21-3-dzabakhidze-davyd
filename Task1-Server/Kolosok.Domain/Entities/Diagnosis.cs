namespace Kolosok.Domain.Entities;

public class Diagnosis : BaseEntity
{
    public string Name { get; set; }
    public string Note { get; set; }
    
    public DateTime DetectionTime { get; set; } = DateTime.Now;
    
    public Guid VictimId { get; set; }
    public Victim Victim { get; set; }
}