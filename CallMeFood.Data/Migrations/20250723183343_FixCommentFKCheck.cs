using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallMeFood.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentFKCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c91ab97-feab-4a5d-b210-15195276e757", "AQAAAAIAAYagAAAAEGzBe4KpVkdlqhk3HHinDZ6Wn9cF3BVsASe5N1IZA8HOqTFNTJE1XLWI1fskrALLfA==", "a877de62-7d83-4877-8fd8-99c0dd3c0721" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01058b64-bee5-4d6c-a58c-d6ab98256905", "AQAAAAIAAYagAAAAEDqRA8He8GqEzNTZY3/as33+IiwVKwC+ZJQ6kvJjgNuIg5d5h3kEPXkoL/8r1eFTDg==", "927fef0b-5051-4a4a-990b-75958936f5bd" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 885, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 885, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 888, DateTimeKind.Utc).AddTicks(6921));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 888, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5530));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5535));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 33, 41, 891, DateTimeKind.Utc).AddTicks(5547));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1041ba2e-e26d-43ef-aa21-2bd7d674b222", "AQAAAAIAAYagAAAAEHfOt5jYAs26dp/gUXeHfQa9m9z8hrMT8l5eXNw66F2nvcBTtsJkAevCyxYww6AtRw==", "384f2dfc-61b0-40b2-a7ed-9433c4acc8ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15f28091-f9e3-4260-88b4-3b637c971ef4", "AQAAAAIAAYagAAAAEOzYtPjzLzjbNx3KWg/1UvvchQyalsZVeakPjSDeXYJAKzqybtg4tUZFt7eFaz2n2w==", "83adee4f-51fd-4549-b1d8-68530b6a1901" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 576, DateTimeKind.Utc).AddTicks(2773));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 576, DateTimeKind.Utc).AddTicks(2775));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 576, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 576, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6534));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6538));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 21, 19, 42, 14, 577, DateTimeKind.Utc).AddTicks(6543));
        }
    }
}
