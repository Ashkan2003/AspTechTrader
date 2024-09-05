﻿// <auto-generated />
using System;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905155223_seedData")]
    partial class seedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.Symbol", b =>
                {
                    b.Property<Guid>("SymbolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChartNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DemandPrice")
                        .HasColumnType("int");

                    b.Property<int?>("DemandVolume")
                        .HasColumnType("int");

                    b.Property<int?>("LastDeal")
                        .HasColumnType("int");

                    b.Property<float?>("LastDealPercentage")
                        .HasColumnType("real");

                    b.Property<int?>("LastPrice")
                        .HasColumnType("int");

                    b.Property<float?>("LastPricePercentage")
                        .HasColumnType("real");

                    b.Property<int?>("OfferPrice")
                        .HasColumnType("int");

                    b.Property<int?>("OfferVolume")
                        .HasColumnType("int");

                    b.Property<int?>("State")
                        .HasColumnType("int");

                    b.Property<string>("SymbolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TheFirst")
                        .HasColumnType("int");

                    b.Property<int?>("TheLeast")
                        .HasColumnType("int");

                    b.Property<int?>("TheMost")
                        .HasColumnType("int");

                    b.Property<int?>("Volume")
                        .HasColumnType("int");

                    b.HasKey("SymbolId");

                    b.ToTable("Symbols");

                    b.HasData(
                        new
                        {
                            SymbolId = new Guid("1146184b-e8f3-4385-a43b-5fef1cbd17df"),
                            ChartNumber = "100",
                            DemandPrice = 100,
                            DemandVolume = 100,
                            LastDeal = 100,
                            LastDealPercentage = 100f,
                            LastPrice = 100,
                            LastPricePercentage = 100f,
                            OfferPrice = 100,
                            OfferVolume = 100,
                            State = 1,
                            SymbolName = "دارایکم",
                            TheFirst = 100,
                            TheLeast = 100,
                            TheMost = 100,
                            Volume = 100
                        },
                        new
                        {
                            SymbolId = new Guid("8605cd04-cade-48ef-ae79-d108652f93fb"),
                            ChartNumber = "100",
                            DemandPrice = 100,
                            DemandVolume = 100,
                            LastDeal = 600,
                            LastDealPercentage = 100f,
                            LastPrice = 150,
                            LastPricePercentage = 100f,
                            OfferPrice = 100,
                            OfferVolume = 100,
                            State = 1,
                            SymbolName = "اختم",
                            TheFirst = 100,
                            TheLeast = 110,
                            TheMost = 100,
                            Volume = 120
                        });
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserProperty")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserSymbol", b =>
                {
                    b.Property<Guid>("SymbolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SymbolId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSymbol");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserSymbol", b =>
                {
                    b.HasOne("AspTechTrader.Core.Domain.Entities.Symbol", null)
                        .WithMany("UserSymbols")
                        .HasForeignKey("SymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspTechTrader.Core.Domain.Entities.User", null)
                        .WithMany("UserSymbols")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.Symbol", b =>
                {
                    b.Navigation("UserSymbols");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.User", b =>
                {
                    b.Navigation("UserSymbols");
                });
#pragma warning restore 612, 618
        }
    }
}
