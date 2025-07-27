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

        //GET: Ingredient/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _ingredientService.GetForEditAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        //POST: Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IngredientEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _ingredientService.UpdateAsync(model);

            return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
        }

        //GET: Ingredient/Delete/5
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
