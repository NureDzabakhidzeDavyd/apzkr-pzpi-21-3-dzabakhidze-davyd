namespace Kolosok.Domain.Exceptions
{
    public class lnvalidPasswordException : BadRequestException
    {
        public lnvalidPasswordException() : base(Resources.Exceptions.InvalidPasswordException_Message)
        {
        }
    }
}
