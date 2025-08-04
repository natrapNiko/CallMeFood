
namespace CallMeFood.Web.Controllers
{
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels;
    using CallMeFood.ViewModels.CategoryViewModels;
    using CallMeFood.ViewModels.RecipeViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class RecipeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IIngredientService _ingredientService;

        public RecipeController(IRecipeService recipeService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, ICommentService commentService, IIngredientService ingredientService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _userManager = userManager;
            _commentService = commentService;
            _ingredientService = ingredientService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;
            string? userId = _userManager.GetUserId(User);

            var pagedRecipes = await _recipeService.GetPagedAsync(page, pageSize, userId);
            var totalRecipes = await _recipeService.GetTotalCountAsync();

            var viewModel = new RecipeListViewModel
            {
                Recipes = pagedRecipes, 
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalRecipes / pageSize),
                PageSize = pageSize
            };

            return View(viewModel);
        }




        //GET: Recipe/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var comments = await _commentService.GetByRecipeIdAsync(id);
            var ingredients = await _ingredientService.GetByRecipeIdAsync(id); 

            var viewModel = new RecipeDetailsViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                CategoryName = recipe.CategoryName,
                AuthorName = recipe.AuthorName,
                CreatedOn = recipe.CreatedOn,
                AuthorId = recipe.AuthorId,
                Comments = comments.ToList(),
                Ingredients = ingredients.ToList(),
                NewCommentContent = string.Empty,
                IsFavorite = recipe.IsFavorite
            };

            return View(viewModel);
        }


        //GET: Recipe/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            var viewModel = new RecipeCreateViewModel
            {
                Categories = categories
                    .Select(c => new CategoryDropDownViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList()
            };

            return View(viewModel);
        }


        //POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //Reload categories on error
                var categories = await _categoryService.GetAllAsync();
                model.Categories = (IEnumerable<ViewModels.CategoryDropDownViewModel>?)categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            await _recipeService.AddAsync(model, userId!);
            return RedirectToAction(nameof(Index));
        }

        //GET: Recipe/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();



            // Get categories
            var categories = await _categoryService.GetAllAsync();

            // Map to ViewModel
            var viewModel = new RecipeEditViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                CategoryId = recipe.CategoryId, 
                ImageUrl = recipe.ImageUrl,
                Categories = categories
            };
            return View(viewModel);
        }

        //POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            await _recipeService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id); //returns RecipeViewModel
            if (recipe == null)
                return NotFound();

            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _recipeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(int id, string content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _commentService.AddAsync(id, userId, content);

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string title)
        {
            var results = await _recipeService.SearchByTitleAsync(title);
            return View("SearchResults", results);
        }

        [HttpGet]
        public async Task<IActionResult> MyRecipes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var recipes = await _recipeService.GetByUserIdAsync(userId);
            return View(recipes);
        }

    }
}
