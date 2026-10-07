using SuperSnakeGameAPI.Core.Domain.IdentityEntities;
using SuperSnakeGameAPI.Core.DTOs;
using System.Security.Claims;

namespace SuperSnakeGameAPI.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user, IEnumerable<string> roles);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
    }
}
