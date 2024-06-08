using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Diagnoses.Commands;

public class DeleteDiagnosisCommand : IRequest<Unit>
{
    public DeleteDiagnosisCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class DeleteDiagnosisCommandHandler : IRequestHandler<DeleteDiagnosisCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDiagnosisCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDiagnosisCommand request, CancellationToken cancellationToken)
    {
        var diagnosisExist = await _unitOfWork.DiagnosisRepository.ExistAsync(p => p.Id == request.Id);

        if (!diagnosisExist)
        {
            throw new DiagnosisNotFoundException(request.Id);
        }
        
        await _unitOfWork.DiagnosisRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}

public class DeleteDiagnosisCommandValidator : AbstractValidator<DeleteDiagnosisCommand>
{
    public DeleteDiagnosisCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}