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
                    ImageUrl = "https://www.allrecipes.com/thmb/GKJL13Wb8TZ9hpJ9c70v0aNXsyQ=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/229063-Classic-Restaurant-Caesar-Salad-ddmfs-4x3-231-89bafa5e54dd4a8c933cf2a5f9f12a6f.jpg",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 1, // e.g., Starter
                    UserId = "seed-user-1",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Homemade Chocolate Cake",
                    Description = "Rich chocolate cake for dessert lovers.",
                    Instructions = "Mix, bake, cool, and frost.",
                    ImageUrl = "https://www.allrecipes.com/thmb/E4m_2-kD9C_w5E9kKa2gxiWAc1o=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/5959101-46973ebb82bc4ec3878c5ae0b128626f.jpg",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 3, // e.g., Desserts
                    UserId = "seed-user-2",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 3,
                    Title = "Spaghetti Carbonara",
                    Description = "Classic Italian pasta dish with eggs, cheese, pancetta, and pepper.",
                    Instructions = "Cook pasta. Sauté pancetta. Mix with eggs and cheese. Combine.",
                    ImageUrl = "https://www.allrecipes.com/thmb/zJzTLhtUWknHXVoFIzysljJ9wR8=/0x512/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/11973-spaghetti-carbonara-ii-DDMFS-4x3-6edea51e421e4457ac0c3269f3be5157.jpg",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 2, // e.g., Main Dishes
                    UserId = "seed-user-1",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 4,
                    Title = "Greek Yogurt Parfait",
                    Description = "Healthy layered snack with yogurt, fruit, and granola.",
                    Instructions = "Layer yogurt, fruit, and granola in a glass. Chill and serve.",
                    ImageUrl = "https://www.allrecipes.com/thmb/psuwM2WSiNw59zyQQJFEO-FehOA=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/5645992-63053852193740bba385f0abb5334ad0.jpg",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 1, // e.g., Starter
                    UserId = "seed-user-2",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 5,
                    Title = "Chicken Stir Fry",
                    Description = "Quick Asian-style stir-fried chicken with vegetables.",
                    Instructions = "Cook chicken. Stir-fry vegetables. Mix with sauce. Serve with rice.",
                    ImageUrl = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F43%2F2022%2F04%2F29%2F223382_chicken-stir-fry_Rita-1x1-1.jpg&q=60&c=sc&poi=auto&orient=true&h=512",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 2, // e.g., Main Dishes
                    UserId = "seed-user-1",
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 6,
                    Title = "Blueberry Pancakes",
                    Description = "Fluffy pancakes loaded with fresh blueberries.",
                    Instructions = "Mix batter. Fold in blueberries. Cook on griddle. Serve warm.",
                    ImageUrl = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F43%2F2022%2F05%2F27%2F686460-todds-famous-blueberry-pancakes-Dianne-1x1-1.jpg&q=60&c=sc&poi=auto&orient=true&h=512",
                    CreatedOn = DateTime.UtcNow,
                    CategoryId = 1, // e.g., Starter
                    UserId = "seed-user-2",
                    IsDeleted = false
                }

            };

            return seedRecipes;
        }
    }
}
