namespace CallMeFood.Common
{
    public static class ValidationConstants
    {
        public static class Recipe
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 80;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;

            public const int InstructionsMinLength = 10;
            public const int InstructionsMaxLength = 5000;

            public const string CreateOnFormat = "yyyy-MM-dd";
            public const int CreatedOnLength = 10;
        }
        public static class Ingredient
        {
            public const int NameIngredientMinLength = 2;
            public const int NameIngredientMaxLength = 50;
            public const int QuantityMinLength = 1;
            public const int QuantityMaxLength = 20;
        }
        public static class Category
        {
            public const int NameCategoryMinLength = 3;
            public const int NameCategoryMaxLength = 20;
        }

        public static class  Comment
        {
            public const int MinCommentLenght = 10;
            public const int MaxCommentLenght = 300;
        }
    }
}
