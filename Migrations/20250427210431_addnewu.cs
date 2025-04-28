using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class addnewu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPosition_Table");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductPosition_Table",
                columns: table => new
                {
                    ProductPosition = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    ProductPositionNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
