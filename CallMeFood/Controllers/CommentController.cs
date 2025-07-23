
namespace CallMeFood.Web.Controllers
{
    using CallMeFood.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int recipeId, string content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Comment content cannot be empty.";
                return RedirectToAction("Details", "Recipe", new { id = recipeId });
            }

            await _commentService.AddAsync(recipeId, userId!, content);
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.DeleteAsync(id);
            return RedirectToAction("Details", "Recipe", new { id = comment.RecipeId });
        }
    }
}
