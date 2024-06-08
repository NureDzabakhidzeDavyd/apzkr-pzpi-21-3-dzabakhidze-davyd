using System.Runtime.Serialization;
using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Validators;
using Kolosok.Domain.Enums;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Commands;

public class CreateBrigadeRescuerCommand : IRequest<BrigadeRescuerResponse>
{
    public ContactRequest Contact { get; set; }
    public string Position { get; set; }
    public string Specialization { get; set; }
    public Guid BrigadeId { get; set; }
    
    [IgnoreDataMember]
    public Role Role { get; set; } = Role.Rescuer;
}

public class CreateBrigadeRescuerCommandHandler : IRequestHandler<CreateBrigadeRescuerCommand, BrigadeRescuerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBrigadeRescuerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BrigadeRescuerResponse> Handle(CreateBrigadeRescuerCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.BrigadeRepository.ExistAsync(brigade => brigade.Id == request.BrigadeId) == false)
        {
            throw new BrigadeNotFoundException(request.BrigadeId);
        }
        
        var brigadeRescuer = _mapper.Map<Domain.Entities.BrigadeRescuer>(request);
        var contact = await _unitOfWork.ContactRepository.CreateAsync(brigadeRescuer.Contact);
        brigadeRescuer.ContactId = contact.Id;
        var newBrigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.CreateAsync(brigadeRescuer);
        await _unitOfWork.SaveChangesAsync();
        var response = _mapper.Map<BrigadeRescuerResponse>(newBrigadeRescuer);
        return response;
    }
}

public class CreateBrigadeRescuerCommandValidator : AbstractValidator<CreateBrigadeRescuerCommand>
{
    public CreateBrigadeRescuerCommandValidator()
    {
        RuleFor(p => p.Contact)
            .NotNull().WithMessage("{PropertyName} is required.")
            .SetValidator(new ContactValidator());
        RuleFor(p => p.Position)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.Specialization)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.BrigadeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}