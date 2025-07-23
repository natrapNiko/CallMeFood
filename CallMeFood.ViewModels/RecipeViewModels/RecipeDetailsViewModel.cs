using CallMeFood.ViewModels.CommentViewModels;

namespace CallMeFood.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string Instructions { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? AuthorId { get; set; }

        public string NewCommentContent { get; set; } = null!;

        //navigation properties
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

    }
}
