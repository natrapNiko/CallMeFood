using CallMeFood.Data.Models;
using CallMeFood.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CallMeFood.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteController(
            IFavoriteService favoriteService,
            UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyFavorites()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoriteService.GetUserFavoritesAsync(userId!);
            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int recipeId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                await _favoriteService.AddAsync(recipeId, userId);
            }

            return RedirectToAction("Index", "Recipe");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int recipeId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                await _favoriteService.RemoveAsync(recipeId, userId);
            }

            return RedirectToAction("Index", "Recipe", new { id = recipeId });
        }
    }
}
