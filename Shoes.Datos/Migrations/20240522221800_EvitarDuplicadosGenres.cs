using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class EvitarDuplicadosGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GnereName",
                table: "Genres",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_GnereName",
                table: "Genres",
                column: "GnereName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genres_GnereName",
                table: "Genres");

            migrationBuilder.AlterColumn<string>(
                name: "GnereName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
