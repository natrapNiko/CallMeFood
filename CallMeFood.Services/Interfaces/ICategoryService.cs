namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.ViewModels.CategoryViewModels;
    using CallMeFood.ViewModels.RecipeViewModels;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<IEnumerable<RecipeViewModel>> GetRecipesByCategoryIdAsync(int categoryId);
        Task<CategoryViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CategoryViewModel model);
        Task UpdateAsync(CategoryViewModel model);
        Task DeleteAsync(int id);
    }
}
