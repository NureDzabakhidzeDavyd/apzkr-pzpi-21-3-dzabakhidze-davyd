using Kolosok.Domain.Entities;
using Action = Kolosok.Domain.Entities.Action;

namespace Kolosok.Infrastructure.Specifications;

public class GetActionFullInformationSpecification : BaseSpecification<Action>
{
    public GetActionFullInformationSpecification()
    {
        AddIncludes(p => p.BrigadeRescuer);
        AddIncludes($"{nameof(Action.BrigadeRescuer)}.{nameof(BrigadeRescuer.Contact)}");
        AddIncludes($"{nameof(Action.BrigadeRescuer)}.{nameof(BrigadeRescuer.Brigade)}");
        AddIncludes(p => p.Victim);
        AddIncludes($"{nameof(Action.Victim)}.{nameof(Victim.Contact)}");
    }
}