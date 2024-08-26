using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CrearRelacionEntreTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Shoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ColorId",
                table: "Shoes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ColorId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Shoes");
        }
    }
}
