using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Queries;

public class GetDiagnosesPageQuery : IRequest<IEnumerable<DiagnosisResponse>>
{
    public ISpecification<Domain.Entities.Diagnosis>[] Specifications { get; private set; }

    public SearchFilter Filters { get; set; }

    public GetDiagnosesPageQuery(SearchFilter filter)
    {
        Filters = filter;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.Diagnosis>[] specifications)
    {
        Specifications = specifications;
    }
}