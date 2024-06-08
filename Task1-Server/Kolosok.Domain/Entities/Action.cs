namespace Kolosok.Domain.Entities;

public class Action : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionTime { get; set; }
    public string ActionType { get; set; }
    public string ActionPlace { get; set; }
    
    public Guid BrigadeRescuerId { get; set; }
    public BrigadeRescuer BrigadeRescuer { get; set; }
    
    public Guid VictimId { get; set; }
    public Victim Victim { get; set; }
}