using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperSnakeGameAPI.Core.Domain.Entities;

namespace SuperSnakeGameAPI.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Players = Set<Player>();
        }
    }
}
