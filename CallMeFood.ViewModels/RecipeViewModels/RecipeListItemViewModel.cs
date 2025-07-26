namespace CallMeFood.ViewModels.RecipeViewModels
{
    public class RecipeListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFavorite { get; set; } 
    }

}
