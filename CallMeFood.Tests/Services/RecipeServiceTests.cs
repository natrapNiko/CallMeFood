using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using CallMeFood.Data;
using CallMeFood.Data.Models;
using CallMeFood.Services;
using CallMeFood.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CallMeFood.ViewModels.RecipeViewModels;

namespace CallMeFood.Tests.Services
{
    public class RecipeServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new ApplicationDbContext(options);
        }

        private IHttpContextAccessor MockHttpContextWithUser(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            var context = new DefaultHttpContext { User = user };
            var accessor = new Mock<IHttpContextAccessor>();
            accessor.Setup(x => x.HttpContext).Returns(context);
            return accessor.Object;
        }

        [Fact]
        public async Task AddAsync_AddsRecipeToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var commentServiceMock = new Mock<ICommentService>();
            var httpContextAccessor = MockHttpContextWithUser("user123");
            var service = new RecipeService(commentServiceMock.Object, httpContextAccessor, context);

            var model = new RecipeCreateViewModel
            {
                Title = "Pizza",
                Description = "Tasty",
                Instructions = "Bake it",
                CategoryId = 1,
                ImageUrl = "pizza.jpg"
            };

            // Act
            await service.AddAsync(model, "user123");

            // Assert
            var recipe = context.Recipes.FirstOrDefault();
            Assert.NotNull(recipe);
            Assert.Equal("Pizza", recipe.Title);
            Assert.Equal("user123", recipe.UserId);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllRecipes()
        {
            // Arrange
            var context = GetDbContext();
            context.Categories.Add(new Category { Id = 1, Name = "Main" });
            context.Users.Add(new ApplicationUser { Id = "u1", UserName = "TestUser" });
            context.Recipes.Add(new Recipe
            {
                Title = "Soup",
                Description = "Hot and delicious",
                Instructions = "Boil and serve",
                CategoryId = 1,
                UserId = "u1",
                CreatedOn = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            var service = new RecipeService(new Mock<ICommentService>().Object, new Mock<IHttpContextAccessor>().Object, context);

            // Act
            var result = (await service.GetAllAsync()).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Soup", result[0].Title);
        }

        [Fact]
        public async Task DeleteAsync_SoftDeletesRecipe()
        {
            // Arrange
            var context = GetDbContext();
            var recipe = new Recipe
            {
                Title = "ToDelete",
                Description = "desc",
                Instructions = "steps",
                UserId = "u1",
                IsDeleted = false
            };
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var service = new RecipeService(new Mock<ICommentService>().Object, new Mock<IHttpContextAccessor>().Object, context);

            // Act
            await service.DeleteAsync(recipe.Id);

            // Assert
            var dbRecipe = await context.Recipes.FindAsync(recipe.Id);
            Assert.True(dbRecipe!.IsDeleted);
        }

        [Fact]
        public async Task GetTotalCountAsync_ReturnsCountOfNonDeleted()
        {
            // Arrange
            var context = GetDbContext();
            context.Recipes.AddRange(
                new Recipe
                {
                    Title = "R1",
                    Description = "d1",
                    Instructions = "i1",
                    UserId = "u1",
                    IsDeleted = false
                },
                new Recipe
                {
                    Title = "R2",
                    Description = "d2",
                    Instructions = "i2",
                    UserId = "u1",
                    IsDeleted = true
                }
            );
            await context.SaveChangesAsync();

            var service = new RecipeService(new Mock<ICommentService>().Object, new Mock<IHttpContextAccessor>().Object, context);

            // Act
            var count = await service.GetTotalCountAsync();

            // Assert
            Assert.Equal(1, count);
        }
    }
}
