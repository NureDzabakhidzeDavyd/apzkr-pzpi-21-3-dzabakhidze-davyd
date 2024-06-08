using System.Security.Claims;
using FluentValidation;
using Kolosok.Application.Contracts.Auth;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Interfaces.Persistence;
using Kolosok.Domain.Exceptions;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Auth.Commands;

public class LoginUserCommand : IRequest<AuthenticatedResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticatedResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthenticatedResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.ContactRepository.GetByFiltersAsync(null, c => c.Email == request.Email);
        if (user is null)
        {
            throw new UserNotFoundException(request.Email);
        }
        
        if(user.Password == null || AuthExtension.VerifyPassword(request.Password, user.Password, user.Salt) == false)
        {
            throw new lnvalidPasswordException();
        }

        var userClaims = new List<Claim>()
        {
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Sid, user.Id.ToString())
        };
        
        var userAccessToken = _tokenService.GenerateAccessToken(userClaims);

        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpireDate = DateTime.UtcNow.AddDays(7);
        await _unitOfWork.ContactRepository.UpdatePropertiesAsync(c => c.Id == user.Id, (c => c.RefreshToken, refreshToken),
            (c => c.RefreshTokenExpiryTime, refreshTokenExpireDate));
        await _unitOfWork.SaveChangesAsync();
        
        return  new AuthenticatedResponse() { Token = userAccessToken, RefreshToken = refreshToken};
    }
}

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty();
        RuleFor(p => p.Password).NotEmpty();
    }
}