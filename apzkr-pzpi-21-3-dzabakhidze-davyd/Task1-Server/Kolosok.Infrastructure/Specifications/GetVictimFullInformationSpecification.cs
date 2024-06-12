using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Specifications;

public class GetVictimFullInformationSpecification : BaseSpecification<Victim>
{
    public GetVictimFullInformationSpecification()
    {
        AddIncludes(p => p.Diagnoses);
        AddIncludes(p => p.Contact);
        AddIncludes(p => p.BrigadeRescuer);
        AddIncludes($"{nameof(Victim.BrigadeRescuer)}.{nameof(BrigadeRescuer.Contact)}");
        AddIncludes($"{nameof(Victim.BrigadeRescuer)}.{nameof(BrigadeRescuer.Brigade)}");
        AddIncludes(p => p.Actions);
    }
}

public class GetVictimQrInformationSpecification : BaseSpecification<Victim>
{
    public GetVictimQrInformationSpecification()
    {
        AddIncludes(p => p.Contact);
    }
}