

namespace CallMeFood.Data
{
    using CallMeFood.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        //DbSets
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //application of configurations
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Seed Identity roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin-id", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-user-id", Name = "User", NormalizedName = "USER" }
            );

            // Seed user-role relationships
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "seed-user-1", RoleId = "role-admin-id" },
                new IdentityUserRole<string> { UserId = "seed-user-2", RoleId = "role-user-id" }
            );
        }
    }
}
