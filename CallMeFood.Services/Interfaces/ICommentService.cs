
namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.ViewModels.CommentViewModels;

    public interface ICommentService
    {
        Task AddAsync(int recipeId, string? userId, string content);
        Task DeleteAsync(int commentId);
        Task<CommentViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<CommentViewModel>> GetByRecipeIdAsync(int recipeId);
    }
}
