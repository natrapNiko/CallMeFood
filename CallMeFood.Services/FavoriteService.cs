namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.RecipeViewModels;

    using Microsoft.EntityFrameworkCore;

    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(int recipeId, string userId)
        {
            //Check if already exists
            bool alreadyFavorited = await _context.Favorites
                .AnyAsync(f => f.RecipeId == recipeId && f.UserId == userId);

            if (!alreadyFavorited)
            {
                var favorite = new Favorite
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int recipeId, string userId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.RecipeId == recipeId && f.UserId == userId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsFavoriteAsync(int recipeId, string userId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.RecipeId == recipeId && f.UserId == userId);
        }

        public async Task<IEnumerable<RecipeListItemViewModel>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => new RecipeListItemViewModel
                {
                    Id = f.Recipe.Id,
                    Title = f.Recipe.Title,
                    ImageUrl = f.Recipe.ImageUrl ?? null!,
                    CategoryName = f.Recipe.Category.Name
                })
                .ToListAsync();
        }
    }
}
