namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.ViewModels.RecipeViewModels;

    public interface IFavoriteService
    {
        Task AddAsync(int recipeId, string userId);
        Task RemoveAsync(int recipeId, string userId);
        Task<bool> IsFavoriteAsync(int recipeId, string userId);
        Task<IEnumerable<RecipeListItemViewModel>> GetUserFavoritesAsync(string userId);
    }

}
