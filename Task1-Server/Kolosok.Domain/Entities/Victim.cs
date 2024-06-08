using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolosok.Domain.Entities
{
    public class Victim : BaseEntity
    {
        public Guid ContactId { get; set; }
        [Required]
        public Contact Contact { get; set; }
        
        public Guid BrigadeRescuerId { get; set; }
        public BrigadeRescuer BrigadeRescuer { get; set; }
        
        public ICollection<Diagnosis> Diagnoses { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
