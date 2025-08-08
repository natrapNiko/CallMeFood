
namespace CallMeFood.Controllers
{
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.CategoryViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET: /Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        //GET: /Category/Recipes/5
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

        //Details (GET)
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var recipes = await _categoryService.GetRecipesByCategoryIdAsync(id);

            var viewModel = new CategoryDetailsViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Recipes = recipes.ToList()
            };

            return View(viewModel);
        }

        //Create (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool created = await _categoryService.CreateAsync(model);

            if (!created)
            {
                ModelState.AddModelError(string.Empty, "Category with this name already exists.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // Edit (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // Edit (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            await _categoryService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Delete (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // Delete (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }


    }
}
