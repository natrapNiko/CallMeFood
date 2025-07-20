
namespace CallMeFood.Web.Controllers
{
    using CallMeFood.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllAsync();
            return View(recipes);
        }
    }
}
