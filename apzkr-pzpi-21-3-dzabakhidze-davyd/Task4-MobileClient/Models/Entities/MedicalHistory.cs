using CareWatch.Mobile.Models.Entities;

namespace CareWatch.Mobile.Models.Entities
{
    public class MedicalHistory : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public string Disease { get; set; }

        public string Treatment { get; set; }

        public Guid AssignedDoctorId { get; set; }
        public Doctor AssignedDoctor { get; set; }
    }
}