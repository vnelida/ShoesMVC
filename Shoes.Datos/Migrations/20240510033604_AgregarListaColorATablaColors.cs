using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarListaColorATablaColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "ColorName" },
                values: new object[,]
                {
                    { 2, "Blanco" },
                    { 3, "Negro" },
                    { 4, "Rojo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: 4);
        }
    }
}
