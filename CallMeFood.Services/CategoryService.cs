
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.CategoryViewModels;
    using CallMeFood.ViewModels.RecipeViewModels;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> CreateAsync(CategoryViewModel model)
        {
            var normalizedName = model.Name.Trim().ToLower();

            bool exists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == normalizedName);

            if (exists)
                return false;

            var category = new Category
            {
                Name = model.Name.Trim()
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task UpdateAsync(CategoryViewModel model)
        {
            var category = await _context.Categories.FindAsync(model.Id);
            if (category == null) return;

            category.Name = model.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hasRecipes = await _context.Recipes.AnyAsync(r => r.CategoryId == id && !r.IsDeleted);

            if (hasRecipes)
            {
                throw new InvalidOperationException("Cannot delete category because it has related recipes.");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<RecipeViewModel>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            return await _context.Recipes
                .Where(r => r.CategoryId == categoryId && !r.IsDeleted)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CategoryName = r.Category.Name,
                    CreatedOn = r.CreatedOn,
                    AuthorName = r.User.UserName ?? "Unknown",
                    ImageUrl = r.ImageUrl,
                    AuthorId = r.UserId
                })
                .ToListAsync();
        }
    }

}
