using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    public partial class ReflectPricingPortfolioMeterChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolio_mpan",
                schema: "pricing");

            migrationBuilder.CreateTable(
                name: "portfolio_meter",
                schema: "pricing",
                columns: table => new
                {
                    meterNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    effectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effectiveTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    meterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    portfolioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolio_meter", x => new { x.meterNumber, x.effectiveFrom, x.effectiveTo });
                    table.UniqueConstraint("AK_portfolio_meter_id_meterNumber", x => new { x.id, x.meterNumber });
                    table.ForeignKey(
                        name: "FK_portfolio_meter_portfolio_portfolioId",
                        column: x => x.portfolioId,
                        principalSchema: "pricing",
                        principalTable: "portfolio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_meter_portfolioId",
                schema: "pricing",
                table: "portfolio_meter",
                column: "portfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolio_meter",
                schema: "pricing");

            migrationBuilder.CreateTable(
                name: "portfolio_mpan",
                schema: "pricing",
                columns: table => new
                {
                    mpanCore = table.Column<string>(nullable: false),
                    effectiveFrom = table.Column<DateTime>(nullable: false),
                    effectiveTo = table.Column<DateTime>(nullable: false),
                    id = table.Column<string>(nullable: false),
                    portfolioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolio_mpan", x => new { x.mpanCore, x.effectiveFrom, x.effectiveTo });
                    table.UniqueConstraint("AK_portfolio_mpan_id_mpanCore", x => new { x.id, x.mpanCore });
                    table.ForeignKey(
                        name: "FK_portfolio_mpan_portfolio_portfolioId",
                        column: x => x.portfolioId,
                        principalSchema: "pricing",
                        principalTable: "portfolio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_mpan_portfolioId",
                schema: "pricing",
                table: "portfolio_mpan",
                column: "portfolioId");
        }
    }
}
