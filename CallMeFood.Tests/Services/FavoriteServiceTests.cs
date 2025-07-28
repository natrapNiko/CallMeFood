using CallMeFood.Data;
using CallMeFood.Data.Models;
using CallMeFood.Services;
using Microsoft.EntityFrameworkCore;

namespace CallMeFood.Tests.Services
{
    public class FavoriteServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new ApplicationDbContext(options);
        }

        private Recipe CreateValidRecipe(int id, string userId = "user1")
        {
            return new Recipe
            {
                Id = id,
                Title = $"Recipe {id}",
                Description = "Test Description",
                Instructions = "Test Instructions",
                UserId = userId,
                CategoryId = 1,
                CreatedOn = DateTime.UtcNow
            };
        }

        private Category CreateCategory() => new Category { Id = 1, Name = "Test Category" };

        [Fact]
        public async Task AddAsync_AddsFavorite_WhenNotAlreadyExists()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe(1);
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            await service.AddAsync(recipe.Id, "user1");

            var favorite = await context.Favorites.FirstOrDefaultAsync();
            Assert.NotNull(favorite);
            Assert.Equal(recipe.Id, favorite.RecipeId);
            Assert.Equal("user1", favorite.UserId);
        }

        [Fact]
        public async Task AddAsync_DoesNotDuplicateFavorite()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe(1);
            context.Recipes.Add(recipe);
            context.Favorites.Add(new Favorite { RecipeId = recipe.Id, UserId = "user1", CreatedOn = DateTime.UtcNow });
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            await service.AddAsync(recipe.Id, "user1");

            var count = await context.Favorites.CountAsync();
            Assert.Equal(1, count); // should not duplicate
        }

        [Fact]
        public async Task RemoveAsync_RemovesExistingFavorite()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe(1);
            context.Recipes.Add(recipe);
            context.Favorites.Add(new Favorite { RecipeId = recipe.Id, UserId = "user1", CreatedOn = DateTime.UtcNow });
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            await service.RemoveAsync(recipe.Id, "user1");

            Assert.Empty(context.Favorites);
        }

        [Fact]
        public async Task RemoveAsync_DoesNothing_WhenFavoriteNotExists()
        {
            var context = GetDbContext();
            var service = new FavoriteService(context);

            // Should not throw
            await service.RemoveAsync(123, "nonexistent");

            Assert.Empty(context.Favorites);
        }

        [Fact]
        public async Task IsFavoriteAsync_ReturnsTrue_IfFavoriteExists()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe(1);
            context.Recipes.Add(recipe);
            context.Favorites.Add(new Favorite { RecipeId = recipe.Id, UserId = "user1", CreatedOn = DateTime.UtcNow });
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            var result = await service.IsFavoriteAsync(recipe.Id, "user1");

            Assert.True(result);
        }

        [Fact]
        public async Task IsFavoriteAsync_ReturnsFalse_IfFavoriteNotExists()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe(1);
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            var result = await service.IsFavoriteAsync(recipe.Id, "user1");

            Assert.False(result);
        }

        [Fact]
        public async Task GetUserFavoritesAsync_ReturnsUserFavorites()
        {
            var context = GetDbContext();

            var category = CreateCategory();
            var recipe = CreateValidRecipe(1);
            recipe.Category = category;

            context.Categories.Add(category);
            context.Recipes.Add(recipe);
            context.Favorites.Add(new Favorite
            {
                RecipeId = recipe.Id,
                UserId = "user1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            var service = new FavoriteService(context);

            var favorites = (await service.GetUserFavoritesAsync("user1")).ToList();

            Assert.Single(favorites);
            Assert.Equal("Recipe 1", favorites[0].Title);
            Assert.Equal("Test Category", favorites[0].CategoryName);
        }
    }
}
