using System.ComponentModel.DataAnnotations;

namespace Kolosok.Domain.Entities;

public class BrigadeRescuer : BaseEntity
{
    public Guid ContactId { get; set; }
    [Required]
    public Contact Contact { get; set; }
    
    public string Position { get; set; }
    public string Specialization { get; set; }
    
    public Brigade Brigade { get; set; }
    public Guid BrigadeId { get; set; }
    
    public ICollection<Action> Actions { get; set; }
    public ICollection<Victim> Victims { get; set; }
}