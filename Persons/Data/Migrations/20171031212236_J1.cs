using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Persons.Data.Migrations
{
    public partial class J1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cotizaciones_IDCotizacion",
                table: "Cotizaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cotizaciones",
                table: "Cotizaciones");

            migrationBuilder.AlterColumn<int>(
                name: "IDCotizacion",
                table: "Cotizaciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cotizaciones",
                table: "Cotizaciones",
                column: "IDCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_IDMoneda",
                table: "Cotizaciones",
                column: "IDMoneda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cotizaciones",
                table: "Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Cotizaciones_IDMoneda",
                table: "Cotizaciones");

            migrationBuilder.AlterColumn<int>(
                name: "IDCotizacion",
                table: "Cotizaciones",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cotizaciones_IDCotizacion",
                table: "Cotizaciones",
                column: "IDCotizacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cotizaciones",
                table: "Cotizaciones",
                columns: new[] { "IDMoneda", "Fecha" });
        }
    }
}
