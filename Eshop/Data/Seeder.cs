using Eshop.Context;
using Eshop.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {



            if (!context.Roles.Any())
            {
                var roles = new List<Roles>
                {
                    new Roles { Role = "Admin" },
                    new Roles { Role = "User" }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }


            if (!context.Auths.Any(u => u.Email == "admin@example.com"))
            {
                var admin = new Auth
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    UserName = "Admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"
                };

                await context.Auths.AddAsync(admin);
                await context.SaveChangesAsync();


                var adminRole = await context.Roles.FirstAsync(r => r.Role == "Admin");
                var userRole = new UserRole
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                };
                await context.UserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();
            }
        }
    }
}
