namespace Kolosok.Domain.Exceptions.NotFound;

public class DiagnosisNotFoundException : NotFoundException
{
    public DiagnosisNotFoundException(Guid id)
        : base(string.Format(Resources.Exceptions.DiagnosisNotFoundException_Message, id))
    {
    }
}