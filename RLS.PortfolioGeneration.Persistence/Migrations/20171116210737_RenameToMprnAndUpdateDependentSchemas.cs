using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class RenameToMprnAndUpdateDependentSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mpnr",
                schema: "clients");

            migrationBuilder.CreateTable(
                name: "mprn",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AQ = table.Column<double>(type: "float", nullable: false),
                    ChangeOfUse = table.Column<bool>(type: "bit", nullable: false),
                    IsImperial = table.Column<bool>(type: "bit", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MprnCore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mprn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mprn_site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "clients",
                        principalTable: "site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mprn_SiteId",
                schema: "clients",
                table: "mprn",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mprn",
                schema: "clients");

            migrationBuilder.CreateTable(
                name: "mpnr",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AQ = table.Column<double>(nullable: false),
                    ChangeOfUse = table.Column<bool>(nullable: false),
                    IsImperial = table.Column<bool>(nullable: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    MprnCore = table.Column<string>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    SiteId = table.Column<Guid>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpnr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mpnr_site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "clients",
                        principalTable: "site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mpnr_SiteId",
                schema: "clients",
                table: "mpnr",
                column: "SiteId");
        }
    }
}
