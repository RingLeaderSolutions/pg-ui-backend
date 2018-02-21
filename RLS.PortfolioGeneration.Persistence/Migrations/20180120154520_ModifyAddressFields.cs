using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class ModifyAddressFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "clients",
                table: "site");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                schema: "clients",
                table: "site",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                schema: "clients",
                table: "site");

            migrationBuilder.DropColumn(
                name: "Address2",
                schema: "clients",
                table: "site");

            migrationBuilder.DropColumn(
                name: "Town",
                schema: "clients",
                table: "site");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "clients",
                table: "site",
                nullable: true);
        }
    }
}
