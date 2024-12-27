using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEndDateAndModifyCoursesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Courses",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Courses",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Courses",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Courses",
                newName: "CreatedAt");
        }
    }
}
