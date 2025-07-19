using CallMeFood.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CallMeFood.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = "seed-user-1",
                UserName = "admin@callmefood.com",
                NormalizedUserName = "ADMIN@CALLMEFOOD.COM",
                Email = "admin@callmefood.com",
                NormalizedEmail = "ADMIN@CALLMEFOOD.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

            var userUser = new ApplicationUser
            {
                Id = "seed-user-2",
                UserName = "user@callmefood.com",
                NormalizedUserName = "USER@CALLMEFOOD.COM",
                Email = "user@callmefood.com",
                NormalizedEmail = "USER@CALLMEFOOD.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            userUser.PasswordHash = hasher.HashPassword(userUser, "User123!");

            entity.HasData(adminUser, userUser);
        }
    }
}
