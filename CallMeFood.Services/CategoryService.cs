
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

        public Task AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<Category?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
                ImageUrl = r.ImageUrl,
                CategoryName = r.Category.Name,
                AuthorName = r.User.UserName ?? null!,
                CreatedOn = r.CreatedOn,
                AuthorId = r.UserId
            })
            .ToListAsync();
        }

        public Task UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }

}
