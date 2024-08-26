using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CambiarPresicionCampoSizeNumberEnTablaSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SizeNumber",
                table: "Sizes",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SizeNumber",
                table: "Sizes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");
        }
    }
}
