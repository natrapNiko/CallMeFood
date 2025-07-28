using CallMeFood.Data;
using CallMeFood.Data.Models;
using CallMeFood.Services;
using CallMeFood.ViewModels.IngredientViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CallMeFood.Tests.Services
{
    public class IngredientServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new ApplicationDbContext(options);
        }

        private Recipe CreateValidRecipe(string userId = "user1")
        {
            return new Recipe
            {
                Title = "Test Recipe",
                Description = "Test Description",
                Instructions = "Test Instructions",
                UserId = userId,
                CategoryId = 1,
                CreatedOn = DateTime.UtcNow
            };
        }

        [Fact]
        public async Task AddAsync_AddsIngredientToDatabase()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe();
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var service = new IngredientService(context);
            var model = new IngredientCreateViewModel
            {
                Name = "Sugar",
                Quantity = "2 tbsp",
                RecipeId = recipe.Id
            };

            await service.AddAsync(model);

            var ingredient = context.Ingredients.FirstOrDefault();
            Assert.NotNull(ingredient);
            Assert.Equal("Sugar", ingredient.Name);
            Assert.Equal("2 tbsp", ingredient.Quantity);
        }

        [Fact]
        public async Task GetByRecipeIdAsync_ReturnsCorrectIngredients()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe("u");
            context.Recipes.Add(recipe);

            context.Ingredients.AddRange(
                new Ingredient { Name = "Salt", Quantity = "1 tsp", Recipe = recipe },
                new Ingredient { Name = "Oil", Quantity = "2 tbsp", Recipe = recipe }
            );
            await context.SaveChangesAsync();

            var service = new IngredientService(context);

            var result = (await service.GetByRecipeIdAsync(recipe.Id)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, i => i.Name == "Salt");
            Assert.Contains(result, i => i.Name == "Oil");
        }

        [Fact]
        public async Task GetForEditAsync_ReturnsCorrectModel()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe("u");
            var ingredient = new Ingredient { Name = "Tomato", Quantity = "3 pcs", Recipe = recipe };
            context.Recipes.Add(recipe);
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            var service = new IngredientService(context);

            var result = await service.GetForEditAsync(ingredient.Id);

            Assert.NotNull(result);
            Assert.Equal("Tomato", result?.Name);
            Assert.Equal("Test Recipe", result?.RecipeTitle);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectModel()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe("u");
            var ingredient = new Ingredient { Name = "Carrot", Quantity = "2 pcs", Recipe = recipe };
            context.Recipes.Add(recipe);
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            var service = new IngredientService(context);

            var result = await service.GetByIdAsync(ingredient.Id);

            Assert.NotNull(result);
            Assert.Equal("Carrot", result?.Name);
            Assert.Equal("Test Recipe", result?.RecipeTitle);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesIngredientCorrectly()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe();
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var ingredient = new Ingredient { Name = "Milk", Quantity = "100 ml", RecipeId = recipe.Id };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            var service = new IngredientService(context);
            var model = new IngredientEditViewModel
            {
                Id = ingredient.Id,
                Name = "Almond Milk",
                Quantity = "150 ml",
                RecipeId = recipe.Id
            };

            await service.UpdateAsync(model);

            var updated = await context.Ingredients.FindAsync(ingredient.Id);
            Assert.Equal("Almond Milk", updated?.Name);
            Assert.Equal("150 ml", updated?.Quantity);
        }

        [Fact]
        public async Task UpdateAsync_ThrowsException_WhenIngredientNotFound()
        {
            var context = GetDbContext();
            var service = new IngredientService(context);

            var model = new IngredientEditViewModel
            {
                Id = 999,
                Name = "Fake",
                Quantity = "0"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(model));
        }

        [Fact]
        public async Task DeleteAsync_RemovesIngredient()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe();
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var ingredient = new Ingredient { Name = "Flour", Quantity = "200g", RecipeId = recipe.Id };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            var service = new IngredientService(context);
            await service.DeleteAsync(ingredient.Id);

            Assert.Empty(context.Ingredients);
        }

        [Fact]
        public async Task DeleteAsync_DoesNothing_IfIngredientNotFound()
        {
            var context = GetDbContext();
            var service = new IngredientService(context);

            await service.DeleteAsync(999); // no exception

            Assert.Empty(context.Ingredients);
        }
    }
}
