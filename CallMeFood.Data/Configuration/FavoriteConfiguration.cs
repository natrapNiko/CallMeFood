
namespace CallMeFood.Data.Configuration
{
    using CallMeFood.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> entity)
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.CreatedOn)
                .IsRequired();

            entity.HasQueryFilter(f => !f.Recipe.IsDeleted);

            entity.HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Recipe)
                .WithMany(r => r.Favorites)
                .HasForeignKey(f => f.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(GenerateSeedFavorites());
        }

        private List<Favorite> GenerateSeedFavorites()
        {
            List<Favorite> seedFavorites = new List<Favorite>()
            {
                new Favorite
                {
                    Id = 1,
                    RecipeId = 1,
                    UserId = "seed-user-2",
                    CreatedOn = DateTime.UtcNow
                },
                new Favorite
                {
                    Id = 2,
                    RecipeId = 2,
                    UserId = "seed-user-1",
                    CreatedOn = DateTime.UtcNow
                }
            };

            return seedFavorites;
        }
    }
}
