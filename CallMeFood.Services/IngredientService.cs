namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.IngredientViewModels;
    using Microsoft.EntityFrameworkCore;

    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IngredientViewModel>> GetByRecipeIdAsync(int recipeId)
        {
            return await _context.Ingredients
                .Where(i => i.RecipeId == recipeId)
                .Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    RecipeId = i.RecipeId
                })
                .ToListAsync();
        }
        public async Task AddAsync(IngredientCreateViewModel model)
        {
            var ingredient = new Ingredient
            {
                Name = model.Name,
                Quantity = model.Quantity,
                RecipeId = model.RecipeId
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<IngredientEditViewModel?> GetForEditAsync(int id)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
                return null;

            return new IngredientEditViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                RecipeId = ingredient.RecipeId,
                RecipeTitle = ingredient.Recipe.Title
            };
        }


        public async Task DeleteAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(IngredientEditViewModel model)
        {
            var ingredient = await _context.Ingredients.FindAsync(model.Id);

            if (ingredient == null)
            {
                throw new InvalidOperationException($"Ingredient with ID {model.Id} not found.");
            }

            ingredient.Name = model.Name;
            ingredient.Quantity = model.Quantity;

            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<IngredientEditViewModel?> GetByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.Recipe)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
                return null;

            return new IngredientEditViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                RecipeId = ingredient.RecipeId,
                RecipeTitle = ingredient.Recipe.Title
            };
        }

    }
}
