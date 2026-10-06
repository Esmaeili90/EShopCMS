using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop_CMS.Migrations
{
    /// <inheritdoc />
    public partial class initfixprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Group");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
