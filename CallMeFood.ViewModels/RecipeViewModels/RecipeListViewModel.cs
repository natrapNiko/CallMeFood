﻿namespace CallMeFood.ViewModels.RecipeViewModels
{
    public class RecipeListViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<RecipeListItemViewModel> Recipes { get; set; } = new List<RecipeListItemViewModel>();
    }
}
