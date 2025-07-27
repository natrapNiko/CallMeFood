
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.RecipeViewModels;
    using Microsoft.EntityFrameworkCore;
    using CallMeFood.ViewModels.CommentViewModels;
    using Microsoft.AspNetCore.Http;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeService(ApplicationDbContext context, ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
        }

        // get all recipes
        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .ToListAsync();

            return recipes.Select(r => new RecipeViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                CategoryName = r.Category?.Name ?? "Unknown",
                AuthorName = r.User?.UserName ?? "Unknown",
                AuthorId = r.User?.Id ?? string.Empty,
                CreatedOn = r.CreatedOn,
                ImageUrl = r.ImageUrl ?? string.Empty
            });
        }

        public async Task AddAsync(RecipeCreateViewModel model, string userId)
        {
            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                IsDeleted = false
            };

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<RecipeDetailsViewModel?> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes
        .Include(r => r.Category)
        .Include(r => r.User)
        .Include(r => r.Comments)
            .ThenInclude(c => c.User)
        .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return null;
            }

            var userId = _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            bool isFavorite = false;

            if (!string.IsNullOrEmpty(userId))
            {
                isFavorite = await _context.Favorites
                    .AnyAsync(f => f.UserId == userId && f.RecipeId == recipe.Id);
            }

            return new RecipeDetailsViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl ?? null!,
                CategoryId = recipe.CategoryId,
                CategoryName = recipe.Category.Name,
                AuthorName = recipe.User.UserName ?? "Unknown",
                AuthorId = recipe.UserId,
                CreatedOn = recipe.CreatedOn,
                Ingredients = recipe.Ingredients.Select(i => i.Name).ToList(),
                Comments = recipe.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    UserName = c.User.UserName
                }).ToList(),

                IsFavorite = isFavorite 
            };
        }
            
        public Task<IEnumerable<Recipe>> GetLatestAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetMostFavoritedAsync(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeListItemViewModel>> GetPagedAsync(int page, int pageSize, string? userId)
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RecipeListItemViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl ?? string.Empty,
                    CategoryName = r.Category.Name,
                    AuthorName = r.User.UserName ?? "Unknown",
                    AuthorId = r.UserId,
                    CreatedOn = r.CreatedOn,
                    IsFavorite = userId != null && r.Favorites.Any(f => f.UserId == userId)
                })
                .ToListAsync();

            return recipes;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted)
                .CountAsync();
        }

        public Task<IEnumerable<Recipe>> SearchByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> SearchByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(RecipeEditViewModel model)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == model.Id && !r.IsDeleted);

            if (recipe == null)
            {
                throw new InvalidOperationException("Recipe not found or is deleted.");
            }

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Instructions = model.Instructions;
            recipe.ImageUrl = model.ImageUrl;
            recipe.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.IsDeleted = true; //soft delete
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Recipe>> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

    }
}
