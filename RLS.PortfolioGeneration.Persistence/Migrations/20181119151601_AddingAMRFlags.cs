using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddingAMRFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "consumption",
                schema: "pricing",
                table: "portfolio_meter",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "pricing",
                table: "portfolio_meter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "utility",
                schema: "pricing",
                table: "portfolio_meter",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "consumption",
                schema: "pricing",
                table: "portfolio_meter");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "pricing",
                table: "portfolio_meter");

            migrationBuilder.DropColumn(
                name: "utility",
                schema: "pricing",
                table: "portfolio_meter");

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
