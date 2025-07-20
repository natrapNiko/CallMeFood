
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // get all recipes
        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Comments)
                .Include(r => r.Favorites)
                .ToListAsync();
        }

        public Task AddAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Recipe> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
