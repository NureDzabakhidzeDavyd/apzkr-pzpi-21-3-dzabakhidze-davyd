using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Action.Commands;

public class DeleteActionCommand : IRequest<Unit>
{
    public DeleteActionCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteActionCommandHandler : IRequestHandler<DeleteActionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteActionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
    {
        var actionExist = await _unitOfWork.ActionRepository.ExistAsync(p => p.Id == request.Id);

        if (!actionExist)
        {
            throw new ActionNotFoundException(request.Id);
        }
        
        await _unitOfWork.ActionRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class DeleteActionCommandValidator : AbstractValidator<DeleteActionCommand>
{
    public DeleteActionCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}