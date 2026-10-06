using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop_CMS.Migrations
{
    /// <inheritdoc />
    public partial class initbrandcredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CreditValue",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditValue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");
        }
    }
}
