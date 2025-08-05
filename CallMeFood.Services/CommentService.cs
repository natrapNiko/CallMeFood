
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.CommentViewModels;
    using Microsoft.EntityFrameworkCore;
    using CallMeFood.Data.Models;

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(int recipeId, string? userId, string content)
        {
            //Validate that the recipe exists
            var recipeExists = await _context.Recipes.AnyAsync(r => r.Id == recipeId);
            if (!recipeExists)
            {
                throw new InvalidOperationException("The recipe does not exist.");
            }

            //Create and add the comment
            var comment = new Comment
            {
                RecipeId = recipeId,
                UserId = userId ?? null!,
                Content = content,
                CreatedOn = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentViewModel?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Where(c => c.Id == id)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    Content = c.Content,
                    AuthorName = c.User.UserName ?? "Unknown",
                    CreatedOn = c.CreatedOn
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CommentViewModel>> GetByRecipeIdAsync(int recipeId)
        {
            return await _context.Comments
                .Where(c => c.RecipeId == recipeId)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    Content = c.Content,
                    AuthorName = c.User.UserName ?? "Unknown",
                    CreatedOn = c.CreatedOn
                })
                .ToListAsync();
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

    }
}
