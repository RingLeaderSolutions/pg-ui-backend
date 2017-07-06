using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.Persistence.Migrations
{
    [DbContext(typeof(ModelDbContext))]
    [Migration("20170705220830_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("clients")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Account", b =>
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

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Mpan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("DataAggregatorMpid");

                    b.Property<string>("DataCollectorMpid");

                    b.Property<string>("EnergisationStatus");

                    b.Property<string>("MeterOperatorMpid");

                    b.Property<string>("MpanCore");

                    b.Property<Guid?>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("mpan");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<Guid?>("AccountId");

                    b.Property<string>("Address");

                    b.Property<decimal>("Capacity");

                    b.Property<string>("CoT");

                    b.Property<string>("Contact");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("site");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Mpan", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Site", "Site")
                        .WithMany("Mpans")
                        .HasForeignKey("SiteId");
                });

            modelBuilder.Entity("RLS.PortfolioGeneration.Persistence.Model.Site", b =>
                {
                    b.HasOne("RLS.PortfolioGeneration.Persistence.Model.Account", "Account")
                        .WithMany("Sites")
                        .HasForeignKey("AccountId");
                });
        }
    }
}
