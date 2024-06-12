using Kolosok.Application.Interfaces.Infrastructure;

namespace Kolosok.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly KolosokDbContext _context;
    
    public IBrigadeRepository BrigadeRepository { get; init; }
    public IBrigadeRescuerRepository BrigadeRescuerRepository { get; init; }
    public IContactRepository ContactRepository { get; init; }
    public IVictimRepository VictimRepository { get; init; }
    public IDiagnosisRepository DiagnosisRepository { get; init; }
    public IActionRepository ActionRepository { get; init; }

    public UnitOfWork(KolosokDbContext context, 
        IBrigadeRepository brigadeRepository, 
        IBrigadeRescuerRepository brigadeRescuerRepository, 
        IContactRepository contactRepository,
        IVictimRepository victimRepository,
        IDiagnosisRepository diagnosisRepository,
        IActionRepository actionRepository)
    {
        _context = context;
        BrigadeRepository = brigadeRepository;
        BrigadeRescuerRepository = brigadeRescuerRepository;
        ContactRepository = contactRepository;
        VictimRepository = victimRepository;
        DiagnosisRepository = diagnosisRepository;
        ActionRepository = actionRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}