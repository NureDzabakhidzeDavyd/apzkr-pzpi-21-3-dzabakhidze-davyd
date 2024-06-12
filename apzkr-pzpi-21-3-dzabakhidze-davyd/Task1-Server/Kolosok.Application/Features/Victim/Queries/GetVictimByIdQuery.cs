using AutoMapper;
using Kolosok.Application.Contracts.Victim;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Victim.Queries;

public class GetVictimByIdQuery : IRequest<VictimResponse>
{
    public Guid Id { get; set; }
    
    public GetVictimByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public void AddSpecification(params ISpecification<Domain.Entities.Victim>[] specifications)
    {
        Specifications = specifications;
    }
    public ISpecification<Domain.Entities.Victim>[] Specifications { get; private set; }
}

public class GetVictimByIdQueryHandler : IRequestHandler<GetVictimByIdQuery, VictimResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVictimByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<VictimResponse> Handle(GetVictimByIdQuery request,
        CancellationToken cancellationToken)
    {
        var victim =
            await _unitOfWork.VictimRepository.GetByFiltersAsync(request.Specifications,
                p => p.Id == request.Id);

        if (victim is null)
        {
            throw new ContactNotFoundException(request.Id);
        }

        var response = _mapper.Map<VictimResponse>(victim);
        return response;
    }
}