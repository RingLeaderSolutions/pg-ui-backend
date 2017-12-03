using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class AddCompanyStatusFieldsToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasCCLException",
                schema: "clients",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFiTException",
                schema: "clients",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegisteredCharity",
                schema: "clients",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVATEligible",
                schema: "clients",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCCLException",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "HasFiTException",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "IsRegisteredCharity",
                schema: "clients",
                table: "account");

            migrationBuilder.DropColumn(
                name: "IsVATEligible",
                schema: "clients",
                table: "account");
        }
    }
}
