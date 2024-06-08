using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolosok.Domain.Entities
{
    public class Brigade : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrigadeSize => BrigadeRescuers?.Count > 0 ? BrigadeRescuers.Count : 0;
        
        public ICollection<BrigadeRescuer> BrigadeRescuers { get; set; }
    }
}
