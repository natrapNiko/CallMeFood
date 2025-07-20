
namespace CallMeFood.Web.Controllers
{
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;

        public RecipeController(IRecipeService recipeService, ICategoryService categoryService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
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

        //GET: Recipe/Create
        public async Task<IActionResult> Create()
        {
            //Get categories for dropdown
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new RecipeCreateViewModel
            {
                Categories = categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList()
            };
            return View(viewModel);
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload categories on error
                var categories = await _categoryService.GetAllAsync();
                model.Categories = categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Requires using System.Security.Claims
            await _recipeService.AddAsync(model, userId!);
            return RedirectToAction(nameof(Index));
        }

    }
}
