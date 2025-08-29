using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Motorcycles.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMens_Rentals_Id",
                table: "DeliveryMens");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Rentals_Id",
                table: "Motorcycles");

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleId1",
                table: "Rentals",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Motorcycles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "CNH_Type",
                table: "DeliveryMens",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DeliveryMens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliveryManId",
                table: "Rentals",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId1",
                table: "Rentals",
                column: "MotorcycleId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_DeliveryMens_DeliveryManId",
                table: "Rentals",
                column: "DeliveryManId",
                principalTable: "DeliveryMens",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId1",
                table: "Rentals",
                column: "MotorcycleId1",
                principalTable: "Motorcycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_DeliveryMens_DeliveryManId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Motorcycles_MotorcycleId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DeliveryManId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MotorcycleId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MotorcycleId1",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Motorcycles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "CNH_Type",
                table: "DeliveryMens",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DeliveryMens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMens_Rentals_Id",
                table: "DeliveryMens",
                column: "Id",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Rentals_Id",
                table: "Motorcycles",
                column: "Id",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
