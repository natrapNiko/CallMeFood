namespace CallMeFood.Services
{
    using CallMeFood.Data.Models;
    using CallMeFood.Data;
    using CallMeFood.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using CallMeFood.ViewModels.RecipeViewModels;

    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(int recipeId, string userId)
        {
            var exists = await _context.Favorites
                .AnyAsync(f => f.RecipeId == recipeId && f.UserId == userId);

            if (!exists)
            {
                _context.Favorites.Add(new Favorite
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow
                });

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
                    Description = f.Recipe.Description
                })
                .ToListAsync();
        }
    }


}
