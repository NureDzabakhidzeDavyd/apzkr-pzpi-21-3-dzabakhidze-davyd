using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Repositories;

public class DiagnosisRepository: BaseRepository<Diagnosis>, IDiagnosisRepository
{
    public DiagnosisRepository(KolosokDbContext context) : base(context)
    {
    }
}