using Microsoft.AspNetCore.Identity;
using BertassoGamingServices.Core.Domain.IdentityEntities;
using BertassoGamingServices.Core.Enums;

namespace BertassoGamingServices.Web.Extensions
{
    public static class SeedAdminExtension
    {
        public static async Task SeedAdminAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (string roleName in Enum.GetNames<UserTypeOptions>())
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }

            string? email = app.Configuration["AdminUser:Email"];
            string? password = app.Configuration["AdminUser:Password"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return;
            }

            ApplicationUser? admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = "Admin",
                };

                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (!result.Succeeded)
                {
                    string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to seed admin user: {errorMessage}");
                }
            }

            string adminRole = UserTypeOptions.Admin.ToString();

            if (!await userManager.IsInRoleAsync(admin, adminRole))
            {
                await userManager.AddToRoleAsync(admin, adminRole);
            }
        }
    }
}
