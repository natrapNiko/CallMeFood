using CallMeFood.Data;
using CallMeFood.Data.Models;
using CallMeFood.Services;
using CallMeFood.ViewModels.CommentViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CallMeFood.Tests.Services
{
    public class CommentServiceTests
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
        public async Task AddAsync_AddsComment_WhenRecipeExists()
        {
            var context = GetDbContext();
            var recipe = CreateValidRecipe("user1");
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            var service = new CommentService(context);

            await service.AddAsync(recipe.Id, "user1", "Nice recipe!");

            var comment = context.Comments.FirstOrDefault();
            Assert.NotNull(comment);
            Assert.Equal("Nice recipe!", comment.Content);
            Assert.Equal("user1", comment.UserId);
        }

        [Fact]
        public async Task AddAsync_ThrowsException_WhenRecipeDoesNotExist()
        {
            var context = GetDbContext();
            var service = new CommentService(context);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.AddAsync(999, "user1", "Invalid recipe"));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectComment()
        {
            var context = GetDbContext();
            var user = new ApplicationUser { Id = "user1", UserName = "TestUser" };
            var recipe = CreateValidRecipe("user1");

            var comment = new Comment
            {
                Recipe = recipe,
                Content = "Hello",
                UserId = "user1",
                CreatedOn = DateTime.UtcNow,
                User = user
            };

            context.Users.Add(user);
            context.Recipes.Add(recipe);
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            var service = new CommentService(context);

            var result = await service.GetByIdAsync(comment.Id);

            Assert.NotNull(result);
            Assert.Equal("Hello", result?.Content);
            Assert.Equal("TestUser", result?.AuthorName);
        }

        [Fact]
        public async Task GetByRecipeIdAsync_ReturnsCommentsForRecipe()
        {
            var context = GetDbContext();
            var user = new ApplicationUser { Id = "user1", UserName = "Tester" };
            var recipe = CreateValidRecipe("user1");

            var comments = new[]
            {
                new Comment { Recipe = recipe, Content = "First", UserId = "user1", CreatedOn = DateTime.UtcNow.AddMinutes(-2), User = user },
                new Comment { Recipe = recipe, Content = "Second", UserId = "user1", CreatedOn = DateTime.UtcNow.AddMinutes(-1), User = user }
            };

            context.Users.Add(user);
            context.Recipes.Add(recipe);
            context.Comments.AddRange(comments);
            await context.SaveChangesAsync();

            var service = new CommentService(context);

            var result = (await service.GetByRecipeIdAsync(recipe.Id)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Second", result[0].Content);
            Assert.Equal("First", result[1].Content);
        }
    }
}
