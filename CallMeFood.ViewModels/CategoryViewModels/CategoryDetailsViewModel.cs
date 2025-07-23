
namespace CallMeFood.ViewModels.CategoryViewModels
{
    using CallMeFood.ViewModels.RecipeViewModels;

    public class CategoryDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<RecipeViewModel> Recipes { get; set; } = new();
    }

}
