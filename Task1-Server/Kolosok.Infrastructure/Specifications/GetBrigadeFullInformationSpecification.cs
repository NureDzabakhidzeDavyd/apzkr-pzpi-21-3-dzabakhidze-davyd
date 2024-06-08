using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Specifications;

public class GetBrigadeFullInformationSpecification : BaseSpecification<Brigade>
{
    public GetBrigadeFullInformationSpecification()
    {
        AddIncludes(p => p.BrigadeRescuers);
        AddIncludes($"{nameof(Brigade.BrigadeRescuers)}.{nameof(BrigadeRescuer.Contact)}");
    }
}