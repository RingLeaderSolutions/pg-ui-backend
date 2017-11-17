using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddMpnrEntityWithRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAggregatorMpid",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "DataCollectorMpid",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "EnergisationStatus",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "MeterOperatorMpid",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AddColumn<string>(
                name: "DAAgent",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DCAgent",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GSPGroup",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnergized",
                schema: "clients",
                table: "mpan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LLF",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOAgent",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementClass",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterTimeSwitchCode",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterType",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileClass",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetrievalMethod",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mpnr",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AQ = table.Column<double>(type: "float", nullable: false),
                    ChangeOfUse = table.Column<bool>(type: "bit", nullable: false),
                    IsImperial = table.Column<bool>(type: "bit", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MpnrCore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpnr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mpnr_site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "clients",
                        principalTable: "site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mpnr_SiteId",
                schema: "clients",
                table: "mpnr",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mpnr",
                schema: "clients");

            migrationBuilder.DropColumn(
                name: "DAAgent",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "DCAgent",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "GSPGroup",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "IsEnergized",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "LLF",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "MOAgent",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "MeasurementClass",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "MeterTimeSwitchCode",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "MeterType",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "ProfileClass",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "RetrievalMethod",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AddColumn<string>(
                name: "DataAggregatorMpid",
                schema: "clients",
                table: "mpan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataCollectorMpid",
                schema: "clients",
                table: "mpan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnergisationStatus",
                schema: "clients",
                table: "mpan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeterOperatorMpid",
                schema: "clients",
                table: "mpan",
                nullable: true);
        }
    }
}
