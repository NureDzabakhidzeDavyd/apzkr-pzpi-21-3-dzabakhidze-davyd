using Kolosok.Domain.Enums;

namespace Kolosok.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public BrigadeRescuer BrigadeRescuer { get; set; }
        public Victim Victim { get; set; }
        
        //Login model
        public byte[]? Salt { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
