using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.DataAcess.Migrations
{
    public partial class reci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Recipes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipes");
        }
    }
}
