using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroindustryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedRealEndDateToWorkerTaskAndInHoursWorkedChangeTypeToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RealEndDate",
                table: "WorkerTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "HoursWorked",
                table: "Workers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealEndDate",
                table: "WorkerTasks");

            migrationBuilder.AlterColumn<double>(
                name: "HoursWorked",
                table: "Workers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
