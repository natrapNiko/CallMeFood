
namespace CallMeFood.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
    }
}
