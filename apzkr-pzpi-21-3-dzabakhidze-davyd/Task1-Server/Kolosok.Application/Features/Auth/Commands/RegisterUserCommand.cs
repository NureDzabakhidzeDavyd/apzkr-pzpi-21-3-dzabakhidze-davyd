using System.Data;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Kolosok.Application.Contracts.Auth;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Interfaces.Persistence;
using Kolosok.Application.Validators;
using Kolosok.Domain.Entities;
using Kolosok.Domain.Enums;
using MediatR;

namespace Kolosok.Application.Features.Auth.Commands;

public class RegisterUserCommand : IRequest<AuthenticatedResponse>
{
    public ContactRequest Contact { get; set; }
    public string Password { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticatedResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<AuthenticatedResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user with the same email already exists
        var existingUser =
            await _unitOfWork.ContactRepository.GetByFiltersAsync(null, user => user.Email == request.Contact.Email);
        if (existingUser != null)
        {
            // throw new UserAlreadyExistsException(request.Email);
            throw new ConstraintException();
        }


        var user = new Contact
        {
            Password = AuthExtension.HashPassword(request.Password, out var salt),
            Salt = salt,
            Role = Role.Rescuer
        };

        _mapper.Map(request.Contact, user);
        user.RefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _unitOfWork.ContactRepository.CreateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate tokens
        var userClaims = new List<Claim>()
        {
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Sid, user.Id.ToString())
        };
        var userAccessToken = _tokenService.GenerateAccessToken(userClaims);

        return new AuthenticatedResponse() { Token = userAccessToken, RefreshToken = user.RefreshToken };
    }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(p => p.Contact)
            .NotNull().WithMessage("{PropertyName} is required.")
            .SetValidator(new ContactValidator());
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}