namespace Kolosok.Application.Contracts;

public class BaseResponse
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}