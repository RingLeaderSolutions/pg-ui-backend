using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddExtraMpanMprnAndSiteFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                schema: "clients",
                table: "mprn",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Capacity",
                schema: "clients",
                table: "mpan",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Connection",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EAC",
                schema: "clients",
                table: "mpan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "REC",
                schema: "clients",
                table: "mpan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                schema: "clients",
                table: "site");

            migrationBuilder.DropColumn(
                name: "Postcode",
                schema: "clients",
                table: "site");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                schema: "clients",
                table: "site");

            migrationBuilder.DropColumn(
                name: "Capacity",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "Connection",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "EAC",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "Postcode",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "REC",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                schema: "clients",
                table: "mprn",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");
        }
    }
}
