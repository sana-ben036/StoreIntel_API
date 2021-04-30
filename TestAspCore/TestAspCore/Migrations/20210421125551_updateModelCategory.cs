using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAspCore.Migrations
{
    public partial class updateModelCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Categories",
                newName: "ImageUrl");
        }
    }
}
