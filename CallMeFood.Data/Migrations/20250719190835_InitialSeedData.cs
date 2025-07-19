using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CallMeFood.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-id", null, "Admin", "ADMIN" },
                    { "role-user-id", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "seed-user-1", 0, "3f1d9ed3-28f8-40a3-b80d-dd3af30e0591", "admin@callmefood.com", true, false, null, "ADMIN@CALLMEFOOD.COM", "ADMIN@CALLMEFOOD.COM", "AQAAAAIAAYagAAAAEEOoZK82R8gZoVKLuqLDI1fZX7k4Tw/rQWSn5N+OWqu9LB88Lar411f718FQl+wNvQ==", null, false, "9ed9d119-b81d-4a71-b269-d9de289c4ada", false, "admin@callmefood.com" },
                    { "seed-user-2", 0, "b69a8b50-b204-4b9b-ab77-5602085479d1", "user@callmefood.com", true, false, null, "USER@CALLMEFOOD.COM", "USER@CALLMEFOOD.COM", "AQAAAAIAAYagAAAAEKtfPVO5icwB3zK8uHJ65h7NjGC0XeupwB3EfEgWJ7Ws6Uvlf1D8LiyvMmiL5yhkAA==", null, false, "232aef32-8faa-4901-a8bf-94ca52f5307f", false, "user@callmefood.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Starter" },
                    { 2, "Main Dishes" },
                    { 3, "Desserts" },
                    { 4, "Drinks" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-admin-id", "seed-user-1" },
                    { "role-user-id", "seed-user-2" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Instructions", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 19, 19, 8, 35, 166, DateTimeKind.Utc).AddTicks(8864), "A timeless salad with romaine, croutons, and parmesan.", null, "Toss all ingredients. Add dressing. Serve chilled.", "Classic Caesar Salad", "seed-user-1" },
                    { 2, 3, new DateTime(2025, 7, 19, 19, 8, 35, 166, DateTimeKind.Utc).AddTicks(8870), "Rich chocolate cake for dessert lovers.", null, "Mix, bake, cool, and frost.", "Homemade Chocolate Cake", "seed-user-2" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 1, "Great salad, easy to make!", new DateTime(2025, 7, 19, 19, 8, 35, 163, DateTimeKind.Utc).AddTicks(5520), 1, "seed-user-2" },
                    { 2, "Cake was super moist and delicious.", new DateTime(2025, 7, 19, 19, 8, 35, 163, DateTimeKind.Utc).AddTicks(5524), 2, "seed-user-1" }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "Id", "CreatedOn", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 19, 19, 8, 35, 164, DateTimeKind.Utc).AddTicks(9062), 1, "seed-user-2" },
                    { 2, new DateTime(2025, 7, 19, 19, 8, 35, 164, DateTimeKind.Utc).AddTicks(9071), 2, "seed-user-1" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Romaine Lettuce", "1 head", 1 },
                    { 2, "Croutons", "1 cup", 1 },
                    { 3, "Parmesan Cheese", "0.5 cup", 1 },
                    { 4, "Chocolate", "200g", 2 },
                    { 5, "Flour", "2 cups", 2 },
                    { 6, "Eggs", "3", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-admin-id", "seed-user-1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-user-id", "seed-user-2" });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin-id");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-user-id");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
