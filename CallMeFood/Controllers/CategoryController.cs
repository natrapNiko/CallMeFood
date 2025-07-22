using CallMeFood.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CallMeFood.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: /Category/Recipes/5
        public async Task<IActionResult> Recipes(int id)
        {
            var recipes = await _categoryService.GetRecipesByCategoryIdAsync(id);
            if (recipes == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = id;
            return View(recipes);
        }
    }
}
