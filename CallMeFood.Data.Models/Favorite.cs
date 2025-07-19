namespace CallMeFood.Data.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
