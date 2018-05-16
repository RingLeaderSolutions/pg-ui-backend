using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clients");

            migrationBuilder.EnsureSchema(
                name: "pricing");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CompanyRegistrationNumber = table.Column<string>(nullable: true),
                    CompanyStatus = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    CreditRating = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RegistrationAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "site",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Capacity = table.Column<decimal>(nullable: false),
                    CoT = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site", x => x.Id);
                });

            ;

            migrationBuilder.CreateTable(
                name: "mpan",
                schema: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataAggregatorMpid = table.Column<string>(nullable: true),
                    DataCollectorMpid = table.Column<string>(nullable: true),
                    EnergisationStatus = table.Column<string>(nullable: true),
                    MeterOperatorMpid = table.Column<string>(nullable: true),
                    MpanCore = table.Column<string>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mpan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mpan_site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "clients",
                        principalTable: "site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tenancy_period",
                schema: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    EffectiveFrom = table.Column<DateTime>(nullable: false),
                    EffectiveTo = table.Column<DateTime>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenancy_period", x => x.id);
                    table.ForeignKey(
                        name: "FK_tenancy_period_account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "clients",
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tenancy_period_site_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "clients",
                        principalTable: "site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_mpan_SiteId",
                schema: "clients",
                table: "mpan",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_tenancy_period_AccountId",
                schema: "clients",
                table: "tenancy_period",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tenancy_period_SiteId",
                schema: "clients",
                table: "tenancy_period",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mpan",
                schema: "clients");

            migrationBuilder.DropTable(
                name: "tenancy_period",
                schema: "clients");

            migrationBuilder.DropTable(
                name: "account",
                schema: "clients");

            migrationBuilder.DropTable(
                name: "site",
                schema: "clients");
        }
    }
}
