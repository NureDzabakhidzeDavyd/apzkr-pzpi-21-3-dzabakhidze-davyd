using AutoMapper;
using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Queries;

public class GetBrigadeRescuerByIdQuery : IRequest<BrigadeRescuerResponse>
{
    public Guid Id { get; set; }
    
    public GetBrigadeRescuerByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public void AddSpecification(params ISpecification<Domain.Entities.BrigadeRescuer>[] specifications)
    {
        Specifications = specifications;
    }
    public ISpecification<Domain.Entities.BrigadeRescuer>[] Specifications { get; private set; }
}

public class GetBrigadeRescuerByIdQueryHandler : IRequestHandler<GetBrigadeRescuerByIdQuery, BrigadeRescuerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBrigadeRescuerByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BrigadeRescuerResponse> Handle(GetBrigadeRescuerByIdQuery request, CancellationToken cancellationToken)
    {
        var brigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.GetByFiltersAsync(request.Specifications, p => p.Id == request.Id) ?? throw new BrigadeNotFoundException(request.Id);
        var response = _mapper.Map<BrigadeRescuerResponse>(brigadeRescuer);
        return response;
    }
}