using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroindustryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkDuralityToResourceAndMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WorkerWorkDuralityPerHectare",
                table: "Resources",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WorkDuralityPerHectare",
                table: "Machines",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkerWorkDuralityPerHectare",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "WorkDuralityPerHectare",
                table: "Machines");
        }
    }
}
