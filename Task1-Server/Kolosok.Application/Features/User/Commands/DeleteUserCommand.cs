using FluentValidation;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolosok.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var contact = await _unitOfWork.ContactRepository.GetByFiltersAsync(null, p => p.Id == request.Id);

            if (contact == null)
            {
                throw new ContactNotFoundException(request.Id);
            }

            await _unitOfWork.ContactRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }

    public class DeleteVictimCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteVictimCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
