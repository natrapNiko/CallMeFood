namespace CallMeFood.Data.Configuration
{
    using CallMeFood.Data.Models;
    using static CallMeFood.Common.ValidationConstants.Comment;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(MaxCommentLenght);

            entity.Property(c => c.CreatedOn)
                .IsRequired();

            entity.HasQueryFilter(c => !c.Recipe.IsDeleted);

            entity.HasOne(c => c.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(GenerateSeedComments());
        }

        private List<Comment> GenerateSeedComments()
        {
            List<Comment> seedComments = new List<Comment>()
            {
                new Comment
                {
                    Id = 1,
                    Content = "Great salad, easy to make!",
                    RecipeId = 1,
                    UserId = "7699db63-964f-7682-82609-d76562e346ce",
                    CreatedOn = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 2,
                    Content = "Cake was super moist and delicious.",
                    RecipeId = 2,
                    UserId = "7699db63-964f-7682-82609-d76562e346ce",
                    CreatedOn = DateTime.UtcNow
                },
            };

            return seedComments;
        }
    }
}
