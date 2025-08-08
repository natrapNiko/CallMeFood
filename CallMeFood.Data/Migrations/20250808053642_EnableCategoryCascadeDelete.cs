using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallMeFood.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnableCategoryCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db63-964f-7682-82609-d76562e346ce",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "813c480c-d839-470a-84bf-3bb1ed5b2aa9", "AQAAAAIAAYagAAAAEFdxPQXgtoT7IWhM38bYjM6xoVYrhmuyQbpj/32A6eWEbLQJqeIQB/FBnPKtpmT5Yw==", "bf2ab2ea-f4a3-4d6b-878e-3c0c54751891" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 981, DateTimeKind.Utc).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 981, DateTimeKind.Utc).AddTicks(7891));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 982, DateTimeKind.Utc).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 982, DateTimeKind.Utc).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7081));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7093));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7096));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 8, 8, 5, 36, 40, 983, DateTimeKind.Utc).AddTicks(7104));

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db63-964f-7682-82609-d76562e346ce",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f404b90-da5a-4574-8ff1-94c7e0e649fd", "AQAAAAIAAYagAAAAEMCL9x4XxcZFLzzUs1WcW3xFjQ4mZRbC35ydxno/Fp3/ZXa+KVDf0IeKjqH7dVM7oA==", "de6ad437-8cc9-4056-95e6-bc5da7d088a1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 252, DateTimeKind.Utc).AddTicks(4953));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 252, DateTimeKind.Utc).AddTicks(4958));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 253, DateTimeKind.Utc).AddTicks(4850));

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 253, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8565));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8569));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8572));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8575));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 25, 19, 50, 35, 254, DateTimeKind.Utc).AddTicks(8582));

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
