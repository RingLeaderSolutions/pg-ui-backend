using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class CleanupAccountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "RegistrationAddress",
                schema: "clients",
                table: "account");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "clients",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                schema: "clients",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IncorporationDate",
                schema: "clients",
                table: "account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                schema: "clients",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "IncorporationDate",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "Postcode",
                schema: "clients",
                table: "account");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "clients",
                table: "account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationAddress",
                schema: "clients",
                table: "account",
                nullable: true);
        }
    }
}
