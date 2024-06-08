namespace Kolosok.Domain.Exceptions.NotFound;

public class VictimNotFoundException : NotFoundException
{
    public VictimNotFoundException(Guid id)
        : base(string.Format(Resources.Exceptions.VictimNotFoundException_Message, id))
    {
    }
}