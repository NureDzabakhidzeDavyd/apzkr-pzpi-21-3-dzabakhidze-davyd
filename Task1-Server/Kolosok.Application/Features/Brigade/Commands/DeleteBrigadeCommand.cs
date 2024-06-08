using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Brigade.Commands;

public class DeleteBrigadeCommand : IRequest<Unit>
{
    public DeleteBrigadeCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteBrigadeCommandHandler : IRequestHandler<DeleteBrigadeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrigadeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBrigadeCommand request, CancellationToken cancellationToken)
    {
        var brigadeExist = await _unitOfWork.BrigadeRepository.ExistAsync(p => p.Id == request.Id);

        if (!brigadeExist)
        {
            throw new BrigadeNotFoundException(request.Id);
        }
        
        await _unitOfWork.BrigadeRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class DeleteBrigadeCommandValidator : AbstractValidator<DeleteBrigadeCommand>
{
    public DeleteBrigadeCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}