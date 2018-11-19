using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddingAMRFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAMR",
                schema: "clients",
                table: "mprn",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAMR",
                schema: "clients",
                table: "mpan",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAMR",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "IsAMR",
                schema: "clients",
                table: "mpan");
        }
    }
}
