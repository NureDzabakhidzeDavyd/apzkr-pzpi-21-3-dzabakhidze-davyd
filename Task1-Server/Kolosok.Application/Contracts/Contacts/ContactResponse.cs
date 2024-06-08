namespace Kolosok.Application.Contracts.Contacts;

public class ContactResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
}