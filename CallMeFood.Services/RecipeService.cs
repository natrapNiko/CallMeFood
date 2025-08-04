
namespace CallMeFood.Services
{
    using CallMeFood.Data;
    using CallMeFood.Data.Models;
    using CallMeFood.Services.Interfaces;
    using CallMeFood.ViewModels.RecipeViewModels;
    using Microsoft.EntityFrameworkCore;
    using CallMeFood.ViewModels.CommentViewModels;
    using Microsoft.AspNetCore.Http;
    using CallMeFood.ViewModels.IngredientViewModels;

    public class RecipeService(ICommentService commentService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) : IRecipeService
    {
        private readonly ICommentService _commentService = commentService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ApplicationDbContext dbContext = dbContext;

        // get all recipes
        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            var recipes = await dbContext.Recipes
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

            await dbContext.Recipes.AddAsync(recipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task<RecipeDetailsViewModel?> GetByIdAsync(int id)
        {
            var recipe = await dbContext.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
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
                isFavorite = await dbContext.Favorites
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
                Ingredients = recipe.Ingredients.Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    RecipeId = i.RecipeId
                }).ToList(),
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
            var recipes = await dbContext.Recipes
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
            return await dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .CountAsync();
        }

        public Task<IEnumerable<Recipe>> SearchByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IEnumerable<RecipeViewModel>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Enumerable.Empty<RecipeViewModel>();
            }

            var recipes = await dbContext.Recipes
                .Where(r => !r.IsDeleted && r.Title.Contains(title))
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
                CreatedOn = r.CreatedOn
            });
        }
        

        public async Task UpdateAsync(RecipeEditViewModel model)
        {
            var recipe = await dbContext.Recipes
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

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await dbContext.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.IsDeleted = true; //soft delete
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RecipeListItemViewModel>> GetByUserIdAsync(string userId)
        {
            return await dbContext.Recipes
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .Select(r => new RecipeListItemViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();
        }
    }
}
