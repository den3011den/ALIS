using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ALIS_DataAccess.Migrations
{
    public partial class addIdentityToKeyFieldIdOfGenreTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Genres",
            //    type: "integer",
            //    nullable: false,
            //    comment: "Идентификатор жанра",
            //    oldClrType: typeof(int),
            //    oldType: "integer",
            //    oldComment: "Идентификатор жанра")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Genres",
            //    type: "integer",
            //    nullable: false,
            //    comment: "Идентификатор жанра",
            //    oldClrType: typeof(int),
            //    oldType: "integer",
            //    oldComment: "Идентификатор жанра")
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);
        }
    }
}
