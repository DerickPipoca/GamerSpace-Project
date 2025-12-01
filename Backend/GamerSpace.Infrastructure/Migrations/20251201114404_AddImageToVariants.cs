using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductVariants",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductVariants");
        }
    }
}
