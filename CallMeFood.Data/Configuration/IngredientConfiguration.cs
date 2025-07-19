
namespace CallMeFood.Data.Configuration
{
    using CallMeFood.Data.Models;
    using static CallMeFood.Common.ValidationConstants.Ingredient;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> entity)
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(NameIngredientMaxLength);

            entity.Property(i => i.Quantity)
                .IsRequired()
                .HasMaxLength(QuantityMaxLength);

            entity.HasQueryFilter(i => !i.Recipe.IsDeleted);

            entity.HasOne(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(GenerateSeedIngredients());
        }

        private List<Ingredient> GenerateSeedIngredients()
        {
            List<Ingredient> seedIngredients = new List<Ingredient>()
            {
                new Ingredient { Id = 1, Name = "Romaine Lettuce", Quantity = "1 head", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Croutons", Quantity = "1 cup", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "Parmesan Cheese", Quantity = "0.5 cup", RecipeId = 1 },
                new Ingredient { Id = 4, Name = "Chocolate", Quantity = "200g", RecipeId = 2 },
                new Ingredient { Id = 5, Name = "Flour", Quantity = "2 cups", RecipeId = 2 },
                new Ingredient { Id = 6, Name = "Eggs", Quantity = "3", RecipeId = 2 }
            };

            return seedIngredients;
        }
    }
}
