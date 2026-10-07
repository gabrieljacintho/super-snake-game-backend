using BertassoGamingServices.Core.Domain.IdentityEntities;
using BertassoGamingServices.Core.DTOs;
using System.Security.Claims;

namespace BertassoGamingServices.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user, IEnumerable<string> roles);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
    }
}
