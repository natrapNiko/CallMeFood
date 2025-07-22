namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.Data.Models;
    using CallMeFood.ViewModels.CategoryViewModels;
    using CallMeFood.ViewModels.RecipeViewModels;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<IEnumerable<RecipeViewModel>> GetRecipesByCategoryIdAsync(int categoryId);
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);

    }

}
