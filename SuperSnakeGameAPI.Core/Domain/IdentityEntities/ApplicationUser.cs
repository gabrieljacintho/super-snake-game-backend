using Microsoft.AspNetCore.Identity;

namespace SuperSnakeGameAPI.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
