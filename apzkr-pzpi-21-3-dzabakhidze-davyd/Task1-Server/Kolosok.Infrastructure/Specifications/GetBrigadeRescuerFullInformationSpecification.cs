using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Specifications;

public class GetBrigadeRescuerFullInformationSpecification : BaseSpecification<BrigadeRescuer>
{
    public GetBrigadeRescuerFullInformationSpecification()
    {
        AddIncludes(p => p.Brigade);
        AddIncludes(p => p.Contact);
        AddIncludes(p => p.Actions);
        AddIncludes($"{nameof(BrigadeRescuer.Actions)}.{nameof(Domain.Entities.Action.Victim)}");
        AddIncludes($"{nameof(BrigadeRescuer.Actions)}.{nameof(Domain.Entities.Action.Victim)}.{nameof(Victim.Contact)}");
    }
}

public class GetBrigadeRescuerQrInformationSpecification : BaseSpecification<BrigadeRescuer>
{
    public GetBrigadeRescuerQrInformationSpecification()
    {
        AddIncludes(p => p.Brigade);
        AddIncludes(p => p.Contact);
    }
}