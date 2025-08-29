using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motorcycles.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MotorcycleId1",
                table: "Rentals");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalValue",
                table: "Rentals",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTime>(
                name: "DevolutionDate",
                table: "Rentals",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevolutionDate",
                table: "Rentals");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalValue",
                table: "Rentals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleId1",
                table: "Rentals",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId1",
                table: "Rentals",
                column: "MotorcycleId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId1",
                table: "Rentals",
                column: "MotorcycleId1",
                principalTable: "Motorcycles",
                principalColumn: "Id");
        }
    }
}
