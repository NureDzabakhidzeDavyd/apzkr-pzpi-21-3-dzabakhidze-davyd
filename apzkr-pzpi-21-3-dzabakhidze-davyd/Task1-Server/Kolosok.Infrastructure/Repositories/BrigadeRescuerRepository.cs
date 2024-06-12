using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Repositories;

public class BrigadeRescuerRepository : BaseRepository<BrigadeRescuer>, IBrigadeRescuerRepository
{
    public BrigadeRescuerRepository(KolosokDbContext context) : base(context)
    {
    }
}