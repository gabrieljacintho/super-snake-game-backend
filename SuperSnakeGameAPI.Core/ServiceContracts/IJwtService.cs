using SuperSnakeGameAPI.Core.Domain.IdentityEntities;
using SuperSnakeGameAPI.Core.DTO;
using System.Security.Claims;

namespace SuperSnakeGameAPI.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
    }
}
