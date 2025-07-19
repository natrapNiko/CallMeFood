namespace CallMeFood.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Quantity { get; set; } = null!;
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
