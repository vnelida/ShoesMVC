using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgrgarDatosATablaSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			decimal talle = 28.0m;
			while (talle <= 50.0m)
			{
				migrationBuilder.InsertData(
					table: "Sizes",
					columns: new[] { "SizeNumber" },
					values: new object[] { talle });

				talle += 0.5m;
			}
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			decimal talle = 28.0m;
			while (talle <= 50.0m)
			{
				migrationBuilder.DeleteData(
					table: "Sizes",
					keyColumn: "SizeId", 
					keyValue: talle); 

				talle += 0.5m;
			}
		}
    }
}
