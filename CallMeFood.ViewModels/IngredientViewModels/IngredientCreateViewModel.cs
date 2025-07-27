namespace CallMeFood.ViewModels.IngredientViewModels
{
    public class IngredientCreateViewModel
    {
        public string Name { get; set; } = null!;
        public string Quantity { get; set; } = null!;
        public int RecipeId { get; set; }
    }
}
