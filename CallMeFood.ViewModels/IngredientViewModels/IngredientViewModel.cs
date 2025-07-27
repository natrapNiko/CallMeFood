namespace CallMeFood.ViewModels.IngredientViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Quantity { get; set; } = null!;
        public int RecipeId { get; set; }
    }
}
