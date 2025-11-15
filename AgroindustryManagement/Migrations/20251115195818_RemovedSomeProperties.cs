using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroindustryManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSomeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToField",
                table: "Machines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedToField",
                table: "Machines",
                type: "INTEGER",
                nullable: true);
        }
    }
}
