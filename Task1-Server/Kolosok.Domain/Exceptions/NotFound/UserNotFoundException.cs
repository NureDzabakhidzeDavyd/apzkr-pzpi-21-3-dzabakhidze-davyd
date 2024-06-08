namespace Kolosok.Domain.Exceptions.NotFound;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email) : base(string.Format(Resources.Exceptions.UserNotFoundException_Message, email))
    {
    }
}