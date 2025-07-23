namespace CallMeFood.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public int RecipeId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }

}
