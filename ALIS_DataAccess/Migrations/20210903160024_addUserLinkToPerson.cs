using Microsoft.EntityFrameworkCore.Migrations;

namespace ALIS_DataAccess.Migrations
{
    public partial class addUserLinkToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_User_id",
                table: "Persons",
                column: "User_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_User_id",
                table: "Persons",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_User_id",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_User_id",
                table: "Persons");
        }
    }
}
