using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Queries;

public class GetDiagnosisByIdQuery : IRequest<DiagnosisResponse>
{
    public Guid Id { get; set; }
    
    public GetDiagnosisByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public void AddSpecification(params ISpecification<Domain.Entities.Diagnosis>[] specifications)
    {
        Specifications = specifications;
    }
    public ISpecification<Domain.Entities.Diagnosis>[] Specifications { get; private set; }

}