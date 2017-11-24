using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class ModifyingSiteEntityToCompositePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mpan_site_SiteId",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropForeignKey(
                name: "FK_mprn_site_SiteId",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropForeignKey(
                name: "FK_tenancy_period_site_SiteId",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropIndex(
                name: "IX_tenancy_period_SiteId",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropPrimaryKey(
                name: "PK_site",
                schema: "clients",
                table: "site");

            migrationBuilder.DropIndex(
                name: "IX_mprn_SiteId",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropIndex(
                name: "IX_mpan_SiteId",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AddColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "tenancy_period",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SiteId1",
                schema: "clients",
                table: "tenancy_period",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "site",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "mprn",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "mpan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_site",
                schema: "clients",
                table: "site",
                columns: new[] { "Id", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_tenancy_period_SiteId1_SiteCode",
                schema: "clients",
                table: "tenancy_period",
                columns: new[] { "SiteId1", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_mprn_SiteId_SiteCode",
                schema: "clients",
                table: "mprn",
                columns: new[] { "SiteId", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_mpan_SiteId_SiteCode",
                schema: "clients",
                table: "mpan",
                columns: new[] { "SiteId", "SiteCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_mpan_site_SiteId_SiteCode",
                schema: "clients",
                table: "mpan",
                columns: new[] { "SiteId", "SiteCode" },
                principalSchema: "clients",
                principalTable: "site",
                principalColumns: new[] { "Id", "SiteCode" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_mprn_site_SiteId_SiteCode",
                schema: "clients",
                table: "mprn",
                columns: new[] { "SiteId", "SiteCode" },
                principalSchema: "clients",
                principalTable: "site",
                principalColumns: new[] { "Id", "SiteCode" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tenancy_period_site_SiteId1_SiteCode",
                schema: "clients",
                table: "tenancy_period",
                columns: new[] { "SiteId1", "SiteCode" },
                principalSchema: "clients",
                principalTable: "site",
                principalColumns: new[] { "Id", "SiteCode" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mpan_site_SiteId_SiteCode",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropForeignKey(
                name: "FK_mprn_site_SiteId_SiteCode",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropForeignKey(
                name: "FK_tenancy_period_site_SiteId1_SiteCode",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropIndex(
                name: "IX_tenancy_period_SiteId1_SiteCode",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropPrimaryKey(
                name: "PK_site",
                schema: "clients",
                table: "site");

            migrationBuilder.DropIndex(
                name: "IX_mprn_SiteId_SiteCode",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropIndex(
                name: "IX_mpan_SiteId_SiteCode",
                schema: "clients",
                table: "mpan");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropColumn(
                name: "SiteId1",
                schema: "clients",
                table: "tenancy_period");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                schema: "clients",
                table: "mprn");

            migrationBuilder.DropColumn(
                name: "SiteCode",
                schema: "clients",
                table: "mpan");

            migrationBuilder.AlterColumn<string>(
                name: "SiteCode",
                schema: "clients",
                table: "site",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_site",
                schema: "clients",
                table: "site",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tenancy_period_SiteId",
                schema: "clients",
                table: "tenancy_period",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_mprn_SiteId",
                schema: "clients",
                table: "mprn",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_mpan_SiteId",
                schema: "clients",
                table: "mpan",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_mpan_site_SiteId",
                schema: "clients",
                table: "mpan",
                column: "SiteId",
                principalSchema: "clients",
                principalTable: "site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_mprn_site_SiteId",
                schema: "clients",
                table: "mprn",
                column: "SiteId",
                principalSchema: "clients",
                principalTable: "site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tenancy_period_site_SiteId",
                schema: "clients",
                table: "tenancy_period",
                column: "SiteId",
                principalSchema: "clients",
                principalTable: "site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
