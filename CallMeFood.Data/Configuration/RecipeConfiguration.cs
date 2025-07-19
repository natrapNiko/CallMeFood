namespace CallMeFood.Data.Configuration
{
    using CallMeFood.Data.Models;
    using static CallMeFood.Common.ValidationConstants.Recipe;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> entity)
        {
            // Primary Key
            entity.HasKey(r => r.Id);

            // Properties
            entity.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            entity.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            entity.Property(r => r.Instructions)
                .IsRequired()
                .HasMaxLength(InstructionsMaxLength);

            entity.Property(r => r.ImageUrl)
                .IsRequired(false);

            entity.Property(r => r.IsDeleted)
                .HasDefaultValue(false);

            // Relationships
            entity.HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(r => r.Comments)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(r => r.Favorites)
                .WithOne(f => f.Recipe)
                .HasForeignKey(f => f.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Global query filter for soft delete
            entity.HasQueryFilter(r => !r.IsDeleted);

            entity.HasData(GenerateSeedRecipes());
        }

        private List<Recipe> GenerateSeedRecipes()
        {
            List<Recipe> seedRecipes = new List<Recipe>()
            {
                new Recipe
                {
                    Id = 1,
                    Title = "Classic Caesar Salad",
                    Description = "A timeless salad with romaine, croutons, and parmesan.",
                    Instructions = "Toss all ingredients. Add dressing. Serve chilled.",
                    ImageUrl = null,
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 1,
                    UserId = "seed-user-1",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Homemade Chocolate Cake",
                    Description = "Rich chocolate cake for dessert lovers.",
                    Instructions = "Mix, bake, cool, and frost.",
                    ImageUrl = null,
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 3,
                    UserId = "seed-user-2",
                    IsDeleted = false
                }
            };

            return seedRecipes;
        }
    }
}
