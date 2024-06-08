using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Specifications;

public class GetDiagnosisFullInformationSpecification: BaseSpecification<Diagnosis>
{
    public GetDiagnosisFullInformationSpecification()
    {
        AddIncludes(p => p.Victim);
        AddIncludes($"{nameof(Diagnosis.Victim)}.{nameof(Victim.Contact)}");
        AddIncludes($"{nameof(Diagnosis.Victim)}.{nameof(Victim.BrigadeRescuer)}");
    }
}