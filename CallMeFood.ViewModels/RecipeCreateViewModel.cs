namespace CallMeFood.ViewModels
{
    public class RecipeCreateViewModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }


        // For dropdowns
        public IEnumerable<CategoryViewModel>? Categories { get; set; }
    }

}
