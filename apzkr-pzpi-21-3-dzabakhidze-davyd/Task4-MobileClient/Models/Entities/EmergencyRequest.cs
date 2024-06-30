
namespace CareWatch.Mobile.Models.Entities
{
    public class EmergencyRequest : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid? AcceptedDoctorId { get; set; }
        public Doctor? AcceptedDoctor { get; set; }

        public string Location { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

}
