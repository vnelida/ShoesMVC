using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CrearRelacionTablaSizeShoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoeSizes",
                columns: table => new
                {
                    ShoeSizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeSizes", x => x.ShoeSizeId);
                    table.ForeignKey(
                        name: "FK_ShoeSizes_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeSizes_ShoeId_SizeId",
                table: "ShoeSizes",
                columns: new[] { "ShoeId", "SizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoeSizes_SizeId",
                table: "ShoeSizes",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeSizes");
        }
    }
}
