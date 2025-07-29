
namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.ViewModels.IngredientViewModels;
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientViewModel>> GetByRecipeIdAsync(int recipeId);
        Task AddAsync(IngredientCreateViewModel model);
        Task<IngredientEditViewModel?> GetForEditAsync(int id);
        Task UpdateAsync(IngredientEditViewModel model);
        Task<IngredientEditViewModel?> GetByIdAsync(int id);
        Task DeleteAsync(int id);

    }

}
