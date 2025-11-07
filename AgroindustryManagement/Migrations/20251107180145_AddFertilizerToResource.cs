using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroindustryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddFertilizerToResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FertilizerPerHectare",
                table: "Resources",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FertilizerPerHectare",
                table: "Resources");
        }
    }
}
