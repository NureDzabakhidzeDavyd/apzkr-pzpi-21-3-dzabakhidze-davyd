using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Victim.Commands;

public class DeleteVictimCommand : IRequest<Unit>
{
    public DeleteVictimCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteVictimCommandHandler : IRequestHandler<DeleteVictimCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVictimCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteVictimCommand request, CancellationToken cancellationToken)
    {
        var victim = await _unitOfWork.VictimRepository.GetByFiltersAsync(null,p => p.Id == request.Id);

        if (victim == null)
        {
            throw new VictimNotFoundException(request.Id);
        }
        
        await _unitOfWork.VictimRepository.DeleteAsync(request.Id);
        await _unitOfWork.ContactRepository.DeleteAsync(victim.ContactId);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class DeleteVictimCommandValidator : AbstractValidator<DeleteVictimCommand>
{
    public DeleteVictimCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}