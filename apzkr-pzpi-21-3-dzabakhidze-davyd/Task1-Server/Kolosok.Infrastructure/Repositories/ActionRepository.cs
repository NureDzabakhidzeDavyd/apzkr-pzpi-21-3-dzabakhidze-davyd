using Kolosok.Application.Interfaces.Infrastructure;
using Action = Kolosok.Domain.Entities.Action;

namespace Kolosok.Infrastructure.Repositories;

public class ActionRepository : BaseRepository<Action>, IActionRepository
{
    public ActionRepository(KolosokDbContext context) : base(context)
    {
    }
}