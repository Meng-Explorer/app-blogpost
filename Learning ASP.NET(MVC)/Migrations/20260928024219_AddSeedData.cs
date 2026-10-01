using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Learning_ASP.NET_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tbl_Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Technology" },
                    { 2, null, "Healthy" },
                    { 3, null, "LifStyle" }
                });

            migrationBuilder.InsertData(
                table: "Tbl_Post",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "FeaturedImage", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, "MengLang", 1, "This is the Tech Post 1 Content", "tech_image.jpg", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tech Post 1 " },
                    { 2, "MengLang", 2, "This is the Health Post 2 Content", "tech_image.jpg", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health Post 2 " },
                    { 3, "MengLang", 3, "This is the LifeStyle Post 3 Content", "health_image.jpg", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "LifeStyle Post 3 " }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tbl_Post",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tbl_Post",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tbl_Post",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tbl_Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tbl_Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tbl_Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
