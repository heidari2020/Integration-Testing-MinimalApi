using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
    string GenerateRefreshToken();
}