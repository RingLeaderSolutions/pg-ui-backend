using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    [DbContext(typeof(ModelDbContext))]
    [Migration("20170804110639_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("clients")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Address");

                    b.Property<string>("CompanyRegistrationNumber");

                    b.Property<string>("CompanyStatus");

                    b.Property<string>("Contact");

                    b.Property<string>("CreditRating");

                    b.Property<string>("Name");

                    b.Property<string>("RegistrationAddress");

                    b.HasKey("Id");

                    b.ToTable("account");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mpan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("DataAggregatorMpid");

                    b.Property<string>("DataCollectorMpid");

                    b.Property<string>("EnergisationStatus");

                    b.Property<string>("MeterOperatorMpid");

                    b.Property<string>("MpanCore")
                        .IsRequired();

                    b.Property<Guid?>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("mpan");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Address");

                    b.Property<decimal>("Capacity");

                    b.Property<string>("CoT");

                    b.Property<string>("Contact");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("site");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.TenancyPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId");

                    b.Property<DateTime>("EffectiveFrom");

                    b.Property<DateTime>("EffectiveTo");

                    b.Property<Guid>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SiteId");

                    b.ToTable("tenancy_period");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Pricing.Portfolio", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("ContractEnd")
                        .HasColumnName("contractEnd");

                    b.Property<DateTime?>("ContractStart")
                        .HasColumnName("contractStart");

                    b.HasKey("Id");

                    b.ToTable("portfolio","pricing");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Pricing.PortfolioMpan", b =>
                {
                    b.Property<string>("MpanCore")
                        .HasColumnName("mpanCore");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnName("effectiveFrom");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnName("effectiveTo");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnName("id");

                    b.Property<string>("portfolioId");

                    b.HasKey("MpanCore", "EffectiveFrom", "EffectiveTo");

                    b.HasAlternateKey("Id", "MpanCore");

                    b.HasIndex("portfolioId");

                    b.ToTable("portfolio_mpan","pricing");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mpan", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", "Site")
                        .WithMany("Mpans")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.TenancyPeriod", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Account", "Account")
                        .WithMany("TenancyPeriods")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", "Site")
                        .WithMany("TenancyPeriods")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Pricing.PortfolioMpan", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Pricing.Portfolio", "Portfolio")
                        .WithMany("PortfolioMpans")
                        .HasForeignKey("portfolioId");
                });
        }
    }
}
