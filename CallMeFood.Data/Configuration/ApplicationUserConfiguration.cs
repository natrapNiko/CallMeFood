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
            entity.HasData(CreateDefaultAUser());
        }

        private ApplicationUser CreateDefaultAUser()
        {
            ApplicationUser adminUser = new ApplicationUser
            {
                Id = "7699db63-964f-7682-82609-d76562e346ce",
                UserName = "user@callmefood.com",
                NormalizedUserName = "USER@CALLMEFOOD.COM",
                Email = "user@callmefood.com",
                NormalizedEmail = "USER@CALLMEFOOD.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword( new ApplicationUser { UserName = "user@callmefood.com" }, "User123!")
            };
            return adminUser;
        }
    }
}
