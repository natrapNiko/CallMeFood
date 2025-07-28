using CallMeFood.Data;
using CallMeFood.Data.Models;
using CallMeFood.Services;
using CallMeFood.Services.Interfaces;
using CallMeFood.ViewModels.CategoryViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CallMeFood.Tests.Services
{
    public class CategoryServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // use unique DB per test
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [Fact]
        public async Task CreateAsync_AddsCategoryToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var service = new CategoryService(context);
            var model = new CategoryViewModel { Name = "Test Category" };

            // Act
            await service.CreateAsync(model);

            // Assert
            Assert.Single(context.Categories);
            Assert.Equal("Test Category", context.Categories.First().Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            // Arrange
            var context = GetDbContext();
            context.Categories.AddRange(new[]
            {
                new Category { Name = "A" },
                new Category { Name = "B" }
            });
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectCategory()
        {
            // Arrange
            var context = GetDbContext();
            var category = new Category { Name = "Test" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            var result = await service.GetByIdAsync(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result?.Name);
        }

        [Fact]
        public async Task UpdateAsync_ChangesCategoryName()
        {
            // Arrange
            var context = GetDbContext();
            var category = new Category { Name = "Old Name" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);
            var updatedModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = "New Name"
            };

            // Act
            await service.UpdateAsync(updatedModel);

            // Assert
            var updated = await context.Categories.FindAsync(category.Id);
            Assert.Equal("New Name", updated?.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCategoryFromDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var category = new Category { Name = "Delete Me" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            await service.DeleteAsync(category.Id);

            // Assert
            Assert.Empty(context.Categories);
        }
    }
}
