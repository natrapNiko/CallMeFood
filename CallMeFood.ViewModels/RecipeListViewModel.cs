namespace CallMeFood.ViewModels
{
    public class RecipeListViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();
    }
}
