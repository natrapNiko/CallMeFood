using CallMeFood.Data.Models;
using CallMeFood.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static CallMeFood.Common.ValidationConstants;

namespace CallMeFood.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteController(IFavoriteService favoriteService, UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _favoriteService.AddAsync(recipeId, userId);
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(); //Handle the edge case when user is not logged in
            }

            await _favoriteService.RemoveAsync(recipeId, userId);
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        public async Task<IActionResult> IsFavoriteAsync(int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(); // or RedirectToAction("Login", "Account");
            }

            await _favoriteService.RemoveAsync(recipeId, userId);
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }
    }
}
