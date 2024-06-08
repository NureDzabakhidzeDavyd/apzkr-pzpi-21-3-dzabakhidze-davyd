namespace Kolosok.Domain.Exceptions.NotFound;

public class ActionNotFoundException : NotFoundException
{
    public ActionNotFoundException(Guid id)
        : base(string.Format(Resources.Exceptions.ActionNotFoundException_Message, id))
    {
    }
}