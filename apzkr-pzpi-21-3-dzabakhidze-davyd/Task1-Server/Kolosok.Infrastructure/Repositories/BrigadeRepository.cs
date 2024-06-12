using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Repositories;

public class BrigadeRepository : BaseRepository<Brigade>, IBrigadeRepository
{
    public BrigadeRepository(KolosokDbContext context) : base(context)
    {
    }
}