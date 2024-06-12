using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.BrigadeRescuer.Commands;

public class DeleteBrigadeRescuerCommand: IRequest<Unit>
{
    public DeleteBrigadeRescuerCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteBrigadeRescuerCommandHandler : IRequestHandler<DeleteBrigadeRescuerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrigadeRescuerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBrigadeRescuerCommand request, CancellationToken cancellationToken)
    {
        var brigadeRescuer = await _unitOfWork.BrigadeRescuerRepository.GetByFiltersAsync(null, p => p.Id == request.Id);

        if (brigadeRescuer is null)
        {
            throw new BrigadeRescuerNotFoundException(request.Id);
        }
        
        await _unitOfWork.BrigadeRescuerRepository.DeleteAsync(request.Id);
        await _unitOfWork.ContactRepository.DeleteAsync(brigadeRescuer.ContactId);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class DeleteBrigadeRescuerCommandValidator : AbstractValidator<DeleteBrigadeRescuerCommand>
{
    public DeleteBrigadeRescuerCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}