using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperSnakeGameAPI.Core.Domain.Entities;
using SuperSnakeGameAPI.Core.Domain.IdentityEntities;

namespace SuperSnakeGameAPI.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Player>(entity =>
            {
                entity.ToTable("Players");

                entity.HasOne<ApplicationUser>()
                    .WithOne()
                    .HasForeignKey<Player>(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
