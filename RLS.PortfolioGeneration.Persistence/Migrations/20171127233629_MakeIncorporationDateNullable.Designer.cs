﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RLS.PortfolioGeneration.Persistence.Model;
using System;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    [DbContext(typeof(ModelDbContext))]
    [Migration("20171127233629_MakeIncorporationDateNullable")]
    partial class MakeIncorporationDateNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("clients")
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanyRegistrationNumber");

                    b.Property<string>("CompanyStatus");

                    b.Property<string>("Contact");

                    b.Property<string>("CountryOfOrigin");

                    b.Property<string>("CreditRating");

                    b.Property<DateTime?>("IncorporationDate");

                    b.Property<string>("Postcode");

                    b.HasKey("Id");

                    b.ToTable("account");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mpan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<decimal>("Capacity");

                    b.Property<string>("Connection");

                    b.Property<string>("DAAgent");

                    b.Property<string>("DCAgent");

                    b.Property<int>("EAC");

                    b.Property<string>("GSPGroup");

                    b.Property<bool>("IsEnergized");

                    b.Property<bool>("IsNewConnection");

                    b.Property<string>("LLF");

                    b.Property<string>("MOAgent");

                    b.Property<string>("MeasurementClass");

                    b.Property<string>("MeterTimeSwitchCode");

                    b.Property<string>("MeterType");

                    b.Property<string>("MpanCore")
                        .IsRequired();

                    b.Property<string>("Postcode");

                    b.Property<string>("ProfileClass");

                    b.Property<int>("REC");

                    b.Property<string>("RetrievalMethod");

                    b.Property<string>("SerialNumber");

                    b.Property<string>("SiteCode");

                    b.Property<Guid?>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("SiteId", "SiteCode");

                    b.ToTable("mpan");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mprn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<double>("AQ");

                    b.Property<bool>("ChangeOfUse");

                    b.Property<bool>("IsImperial");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("MprnCore")
                        .IsRequired();

                    b.Property<string>("SerialNumber");

                    b.Property<string>("SiteCode");

                    b.Property<Guid?>("SiteId");

                    b.Property<decimal>("Size");

                    b.HasKey("Id");

                    b.HasIndex("SiteId", "SiteCode");

                    b.ToTable("mprn");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id");

                    b.Property<string>("SiteCode");

                    b.Property<string>("Address");

                    b.Property<string>("BillingAddress");

                    b.Property<string>("CoT");

                    b.Property<string>("Contact");

                    b.Property<string>("Name");

                    b.Property<string>("Postcode");

                    b.HasKey("Id", "SiteCode");

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

                    b.Property<string>("SiteCode");

                    b.Property<Guid>("SiteId");

                    b.Property<Guid?>("SiteId1");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SiteId1", "SiteCode");

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

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Pricing.PortfolioMeter", b =>
                {
                    b.Property<string>("MeterNumber")
                        .HasColumnName("meterNumber");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnName("effectiveFrom");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnName("effectiveTo");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnName("id");

                    b.Property<string>("MeterType")
                        .HasColumnName("meterType");

                    b.Property<string>("portfolioId");

                    b.HasKey("MeterNumber", "EffectiveFrom", "EffectiveTo");

                    b.HasAlternateKey("Id", "MeterNumber");

                    b.HasIndex("portfolioId");

                    b.ToTable("portfolio_meter","pricing");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mpan", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", "Site")
                        .WithMany("Mpans")
                        .HasForeignKey("SiteId", "SiteCode");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.Mprn", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", "Site")
                        .WithMany("Mprns")
                        .HasForeignKey("SiteId", "SiteCode");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Clients.TenancyPeriod", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Account", "Account")
                        .WithMany("TenancyPeriods")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Clients.Site", "Site")
                        .WithMany("TenancyPeriods")
                        .HasForeignKey("SiteId1", "SiteCode");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Pricing.PortfolioMeter", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Pricing.Portfolio", "Portfolio")
                        .WithMany("PortfolioMeters")
                        .HasForeignKey("portfolioId");
                });
#pragma warning restore 612, 618
        }
    }
}
