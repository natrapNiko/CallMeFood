
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

        //GET: Recipe
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllAsync();
            return View(recipes);
        }

        //GET: Recipe/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            //Check if the recipe exists
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }
    }
}
