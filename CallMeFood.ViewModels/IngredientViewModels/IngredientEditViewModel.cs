
namespace CallMeFood.ViewModels.IngredientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class IngredientEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Quantity { get; set; } = null!;

        public int RecipeId { get; set; }

        public string RecipeTitle { get; set; } = null!;
    }
}