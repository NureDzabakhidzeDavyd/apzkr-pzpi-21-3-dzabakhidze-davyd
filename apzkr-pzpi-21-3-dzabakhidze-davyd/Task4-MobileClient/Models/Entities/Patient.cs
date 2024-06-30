using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models.Entities
{
    public class Patient : BaseEntity
    {
        public Contact Contact { get; set; }

        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; }
    }
}
