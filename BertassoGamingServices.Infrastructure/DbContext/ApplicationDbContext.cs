using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BertassoGamingServices.Core.Domain.Entities;
using BertassoGamingServices.Core.Domain.IdentityEntities;

namespace BertassoGamingServices.Infrastructure.DbContext
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

                entity.HasOne(p => p.User)
                    .WithOne()
                    .HasForeignKey<Player>(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
