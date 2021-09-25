using Microsoft.EntityFrameworkCore.Migrations;

namespace ALIS_DataAccess.Migrations
{
    public partial class AddPhotoPathToPersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Persons",
                type: "character varying",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Persons");
        }
    }
}
