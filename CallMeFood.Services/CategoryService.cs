
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }
    }

}
