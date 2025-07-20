
namespace CallMeFood.Services.Interfaces
{
    using CallMeFood.ViewModels;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    }

}
