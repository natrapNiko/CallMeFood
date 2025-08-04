
namespace CallMeFood.ViewModels.IngredientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class IngredientEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        public string Quantity { get; set; } = null!;

        //To redirect back to Recipe Details
        public int RecipeId { get; set; }
    }

}