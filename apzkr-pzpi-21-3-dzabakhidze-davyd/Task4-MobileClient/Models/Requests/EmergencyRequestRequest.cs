using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CareWatch.Mobile.Models.Requests
{
    public class EmergencyRequestRequest
    {
        public Guid PatientId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
