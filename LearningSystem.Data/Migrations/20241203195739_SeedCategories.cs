using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearningSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IconUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.reshot.com/preview-assets/icons/6WRACM8V2S/code-learning-6WRACM8V2S.svg", "Programming" },
                    { 2, "https://www.reshot.com/preview-assets/icons/U8KFG2YBNS/modern-art-U8KFG2YBNS.svg", "Design" },
                    { 3, "https://www.reshot.com/preview-assets/icons/ZMDWAS6837/educational-marketing-ZMDWAS6837.svg", "Marketing" },
                    { 4, "https://www.reshot.com/preview-assets/icons/BTWNSGCXD5/economic-education-BTWNSGCXD5.svg", "Business" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
