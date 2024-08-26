using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class EvitarDuplicadosTablaColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorName",
                table: "Colors",
                column: "ColorName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colors_ColorName",
                table: "Colors");

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
