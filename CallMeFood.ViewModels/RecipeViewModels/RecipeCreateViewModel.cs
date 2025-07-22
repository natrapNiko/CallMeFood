using System.ComponentModel.DataAnnotations;

namespace CallMeFood.ViewModels.RecipeViewModels
{
    public class RecipeCreateViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Instructions are required.")]
        public string Instructions { get; set; } = null!;
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }


        // For dropdowns Menu
        public IEnumerable<CategoryDropDownViewModel>? Categories { get; set; }
    }

}
