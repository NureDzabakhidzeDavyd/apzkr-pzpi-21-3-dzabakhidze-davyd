using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Interfaces.Infrastructure;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Queries;

public class GetBrigadeRescuerPageQuery : IRequest<IEnumerable<BrigadeRescuerResponse>>
{
    public ISpecification<Domain.Entities.BrigadeRescuer>[] Specifications { get; private set; }

    public SearchFilter Filters { get; set; }

    public GetBrigadeRescuerPageQuery(SearchFilter filter)
    {
        Filters = filter;
    }

    public void AddSpecification(params ISpecification<Domain.Entities.BrigadeRescuer>[] specifications)
    {
        Specifications = specifications;
    } 
}