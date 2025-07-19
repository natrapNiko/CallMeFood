namespace CallMeFood.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public bool IsDeleted { get; set; } // Soft delete

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        //navi
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
    }
}
