
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
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
                CreatedOn = r.CreatedOn,
                ImageUrl = r.ImageUrl ?? string.Empty
            });
        }

        public Task AddAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<RecipeDetailsViewModel?> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                return null;

            return new RecipeDetailsViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                CategoryName = recipe.Category?.Name ?? "Unknown",
                AuthorName = recipe.User?.UserName ?? "Unknown",
                CreatedOn = recipe.CreatedOn,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl ?? string.Empty,
                Ingredients = recipe.Ingredients.Select(i => i.Quantity + " " + i.Name).ToList(),
                Comments = recipe.Comments.Select(c => c.Content).ToList()
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

        public Task<IEnumerable<Recipe>> GetPagedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
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
    }
}
