
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.RecipeViewModels;
    using Microsoft.EntityFrameworkCore;
    using CallMeFood.ViewModels.CommentViewModels;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentService _commentService;

        public RecipeService(ApplicationDbContext context, ICommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        // get all recipes
        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .ToListAsync();

            return recipes.Select(r => new RecipeViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                CategoryName = r.Category?.Name ?? "Unknown",
                AuthorName = r.User?.UserName ?? "Unknown",
                AuthorId = r.User?.Id ?? string.Empty,
                CreatedOn = r.CreatedOn,
                ImageUrl = r.ImageUrl ?? string.Empty
            });
        }

        public async Task AddAsync(RecipeCreateViewModel model, string userId)
        {
            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                IsDeleted = false
            };

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RecipeEditViewModel model)
        {
            var recipe = await _context.Recipes.FindAsync(model.Id);

            if (recipe == null || recipe.IsDeleted)
                throw new ArgumentException("Recipe not found.");

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Instructions = model.Instructions;
            recipe.CategoryId = model.CategoryId;
            recipe.ImageUrl = model.ImageUrl;
            // Optionally: recipe.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<RecipeDetailsViewModel?> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.User) // IMPORTANT
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (recipe == null) return null;

            return new RecipeDetailsViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                CategoryId = recipe.CategoryId,
                CategoryName = recipe.Category.Name,
                AuthorName = recipe.User.UserName ?? "Unknown",
                AuthorId = recipe.UserId,
                CreatedOn = recipe.CreatedOn,
                ImageUrl = recipe.ImageUrl = null!,
                Ingredients = recipe.Ingredients.Select(i => $"{i.Quantity} {i.Name}").ToList(),
                Comments = recipe.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    UserName = c.User?.UserName ?? "Unknown" // SAFE HERE
                }).ToList()
            };
        }


        public Task<IEnumerable<Recipe>> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetLatestAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetMostFavoritedAsync(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeViewModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CategoryName = r.Category.Name,
                    AuthorName = r.User.UserName ?? "Unknown",
                    CreatedOn = r.CreatedOn,
                    ImageUrl = r.ImageUrl,
                    AuthorId = r.UserId
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted)
                .CountAsync();
        }

        public Task<IEnumerable<Recipe>> SearchByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> SearchByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.IsDeleted = true; // soft delete
                await _context.SaveChangesAsync();
            }
        }
    }
}
