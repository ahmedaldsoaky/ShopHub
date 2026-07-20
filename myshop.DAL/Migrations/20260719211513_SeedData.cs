using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myshop.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedTime", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronic devices and accessories", "Electronics" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clothing and accessories", "Fashion" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books and educational materials", "Books" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home and kitchen products", "Home" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgPath", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Lenovo IdeaPad 5", "Images/Products/laptop.jpg", "Laptop Lenovo", 25000m },
                    { 2, 1, "Logitech Wireless Mouse", "Images/Products/mouse.jpg", "Wireless Mouse", 600m },
                    { 3, 2, "100% Cotton", "Images/Products/tshirt.jpg", "Men T-Shirt", 350m },
                    { 4, 3, "Robert C. Martin", "Images/Products/cleancode.jpg", "Clean Code", 850m },
                    { 5, 4, "Automatic Coffee Machine", "Images/Products/coffee.jpg", "Coffee Maker", 3200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

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
