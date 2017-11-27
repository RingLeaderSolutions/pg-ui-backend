using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddMpanIsNewConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AddColumn<bool>(
                name: "IsNewConnection",
                schema: "clients",
                table: "mpan",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNewConnection",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "clients",
                table: "mpan",
                nullable: true);
        }
    }
}
