namespace CareWatch.Mobile.Models.Entities
{
    public class Doctor : BaseEntity
    {
        public Contact Contact { get; set; }

        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; }
    }
}
