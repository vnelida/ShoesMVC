using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class EvitarDuplicadosTablaShoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Shoes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_Description",
                table: "Shoes",
                column: "Description",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shoes_Description",
                table: "Shoes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
