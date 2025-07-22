using CallMeFood.ViewModels.CategoryViewModels;

namespace CallMeFood.ViewModels.RecipeViewModels
{
    public class RecipeEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }

        public IEnumerable<CategoryViewModel>? Categories { get; set; }
    }
}
