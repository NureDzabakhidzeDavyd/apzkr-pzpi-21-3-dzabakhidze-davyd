using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Repositories;

public class VictimRepository : BaseRepository<Victim>, IVictimRepository
{
    public VictimRepository(KolosokDbContext context) : base(context)
    {
    }
}