namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.Data.Models;
    using CallMeFood.ViewModels.RecipeViewModels;

    public interface IRecipeService
    {
        //Basic CRUD
        Task<IEnumerable<RecipeViewModel>> GetAllAsync(); //yes
        Task<RecipeDetailsViewModel?> GetByIdAsync(int id); //yes
        Task AddAsync(RecipeCreateViewModel model, string userId); //yes
        Task UpdateAsync(RecipeEditViewModel model); // yes
        Task DeleteAsync(int id); //yes

        //Pagination and Filtering
        Task<IEnumerable<RecipeListItemViewModel>> GetPagedAsync(int pageNumber, int pageSize, string? userId); //yes
        Task<int> GetTotalCountAsync(); //yes

        //Search
        Task<IEnumerable<RecipeViewModel>> SearchByTitleAsync(string title); //yes

        Task<IEnumerable<Recipe>> SearchByCategoryAsync(int categoryId); //no

        Task<IEnumerable<RecipeListItemViewModel>> GetByUserIdAsync(string userId); //MyRecipe
    }
}
