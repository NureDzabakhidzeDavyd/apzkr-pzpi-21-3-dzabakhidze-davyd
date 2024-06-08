namespace Kolosok.Application.Interfaces.Infrastructure;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();

    public IBrigadeRepository BrigadeRepository { get; init; }
    public IBrigadeRescuerRepository BrigadeRescuerRepository { get; init; }
    public IContactRepository ContactRepository { get; init; }
    public IVictimRepository VictimRepository { get; init; }
    public IDiagnosisRepository DiagnosisRepository { get; init; }
    public IActionRepository ActionRepository { get; init; }
}