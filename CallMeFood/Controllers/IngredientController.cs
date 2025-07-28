namespace CallMeFood.Web.Controllers
{
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.IngredientViewModels;
    using Microsoft.AspNetCore.Mvc;
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        //GET: Ingredient/Create?recipeId=5
        [HttpGet]
        public IActionResult Create(int recipeId)
        {
            var model = new IngredientCreateViewModel { RecipeId = recipeId };
            return View(model);
        }

        //POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _ingredientService.AddAsync(model);
            return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _ingredientService.GetForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IngredientEditViewModel model)
        {
            Console.WriteLine($"Editing ingredient ID: {model.Id}"); // Debug output

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state invalid"); // Debug output
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            try
            {
                await _ingredientService.UpdateAsync(model);
                return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit POST: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _ingredientService.GetForEditAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        //POST: Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _ingredientService.GetByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            int recipeId = ingredient.RecipeId;

            await _ingredientService.DeleteAsync(id);

            return RedirectToAction("Details", "Recipe", new { id = recipeId }); 
        }

    }
}
