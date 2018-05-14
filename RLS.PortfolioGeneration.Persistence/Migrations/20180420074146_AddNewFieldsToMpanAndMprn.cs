using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddNewFieldsToMpanAndMprn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CCLEligible",
                schema: "clients",
                table: "mprn",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactTelephone",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VATPercentage",
                schema: "clients",
                table: "mprn",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "CCLEligible",
                schema: "clients",
                table: "mpan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "VATPercentage",
                schema: "clients",
                table: "mpan",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "CCLEligible",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "ContactName",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "ContactTelephone",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "VATPercentage",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "CCLEligible",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "VATPercentage",
                schema: "clients",
                table: "mpan");
        }
    }
}
