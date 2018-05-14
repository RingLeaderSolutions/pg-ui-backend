using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class RenameEmergencyContactMprnFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
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

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactAddress",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactName",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactTelephone",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyContactAddress",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "EmergencyContactName",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "EmergencyContactTelephone",
                schema: "clients",
                table: "mprn");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "clients",
                table: "mprn",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                schema: "clients",
                table: "mprn",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactTelephone",
                schema: "clients",
                table: "mprn",
                nullable: true);
        }
    }
}
