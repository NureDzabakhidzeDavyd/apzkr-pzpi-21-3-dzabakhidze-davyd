namespace Kolosok.Domain.Exceptions.NotFound;

public class BrigadeRescuerNotFoundException : NotFoundException
{
    public BrigadeRescuerNotFoundException(Guid id)
        : base(string.Format(Resources.Exceptions.BrigadeRescuerNotFoundException_Message, id))
    {
    }
}