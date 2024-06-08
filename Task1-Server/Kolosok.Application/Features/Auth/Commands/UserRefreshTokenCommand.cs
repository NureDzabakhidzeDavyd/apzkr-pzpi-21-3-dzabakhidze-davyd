using FluentValidation;
using Kolosok.Application.Contracts.Auth;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Interfaces.Persistence;
using Kolosok.Domain.Exceptions.NotFound;
using MediatR;

namespace Kolosok.Application.Features.Auth.Commands;

public class UserRefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class UserRefreshTokenCommandHandler : IRequestHandler<UserRefreshTokenCommand, AuthenticatedResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public UserRefreshTokenCommandHandler(ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticatedResponse> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        //if (request is null)
        //    return BadRequest("Invalid client request");
        
        var accessToken = request.AccessToken;
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        var email = principal.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;
        if (email is null)
        {
            throw new Exception();
        }
        var contact = await _unitOfWork.ContactRepository.GetByFiltersAsync(null, c => c.Email == email);

        if (contact is null || contact.RefreshToken != request.RefreshToken ||
            contact.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new UserNotFoundException("User not found or invalid refresh token");
        }
        
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);

        var authenticatedResponse = new AuthenticatedResponse()
        {
            Token = newAccessToken,
            RefreshToken = request.RefreshToken
        };
        
        return authenticatedResponse;
    }
}

public class UserRefreshTokenCommandValidator : AbstractValidator<UserRefreshTokenCommand>
{
    public UserRefreshTokenCommandValidator()
    {
        RuleFor(p => p.AccessToken).NotEmpty();
        RuleFor(p => p.RefreshToken).NotEmpty();
    }
}