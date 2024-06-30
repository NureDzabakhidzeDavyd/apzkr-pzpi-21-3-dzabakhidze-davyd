using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models.Requests
{
    public class MedicalHistoryRequest
    {
        public Guid PatientId { get; set; }
        public Guid AssignedDoctorId { get; set; }
        public string Disease { get; set; }
        public string Treatment { get; set; }
    }
}
