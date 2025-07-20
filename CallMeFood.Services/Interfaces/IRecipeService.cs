
namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.Data.Models;
    using CallMeFood.ViewModels;

    public interface IRecipeService
    {
        //Basic CRUD
        Task<IEnumerable<RecipeViewModel>> GetAllAsync(); //yes
        Task<RecipeDetailsViewModel?> GetByIdAsync(int id); //yes
        Task AddAsync(RecipeCreateViewModel model, string userId); //yes
        Task UpdateAsync(RecipeEditViewModel model);

        Task DeleteAsync(int id);

        //Pagination and Filtering
        Task<IEnumerable<Recipe>> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();

        //Search
        Task<IEnumerable<Recipe>> SearchByTitleAsync(string title);
        Task<IEnumerable<Recipe>> SearchByCategoryAsync(int categoryId);

        //For User-specific recipes
        Task<IEnumerable<Recipe>> GetByUserIdAsync(string userId);

        //Get latest recipes (for home page, etc.)
        Task<IEnumerable<Recipe>> GetLatestAsync(int count);

        //Get popular recipes (for statistics or trending)
        Task<IEnumerable<Recipe>> GetMostFavoritedAsync(int count);
    }
}
