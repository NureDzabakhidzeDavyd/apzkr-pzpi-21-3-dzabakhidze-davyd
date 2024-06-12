namespace Kolosok.Domain.Exceptions.NotFound;

public sealed class ContactNotFoundException : NotFoundException
{
    public ContactNotFoundException(Guid contactId)
        : base(string.Format(Resources.Exceptions.ContactNotFoundException_Message, contactId))
    {
    }
}