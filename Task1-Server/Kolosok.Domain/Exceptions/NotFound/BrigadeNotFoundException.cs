namespace Kolosok.Domain.Exceptions.NotFound;

public sealed class BrigadeNotFoundException : NotFoundException
{
    public BrigadeNotFoundException(Guid contactId)
        : base(string.Format(Resources.Exceptions.BrigadeNotFoundException_Message, contactId))
    {
    }
}