using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException($"User with email {request.Email} not found.");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException("The password is incorrect..");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Your account has been deactivated.");

        var token = await _jwtService.GenerateTokenAsync(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        return new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            Email = user.Email ?? string.Empty,
            FullName = $"{user.FirstName} {user.LastName}"
        };
    }
}