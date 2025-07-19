
namespace CallMeFood.Data.Configuration
{
    using CallMeFood.Data.Models;
    using static CallMeFood.Common.ValidationConstants.Category;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameCategoryMaxLength);

            entity.HasMany(c => c.Recipes)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(GenerateSeedCategories());
        }

        private List<Category> GenerateSeedCategories()
        {
            List<Category> seedCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Starter" },
                new Category { Id = 2, Name = "Main Dishes" },
                new Category { Id = 3, Name = "Desserts" },
                new Category { Id = 4, Name = "Drinks" }
            };

            return seedCategories;
        }
    }
}
