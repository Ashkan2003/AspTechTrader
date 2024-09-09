﻿// <auto-generated />
using System;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Symbol", (string)null);

                    b.HasData(
                        new
                        {
                            SymbolId = new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"),
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
                            SymbolId = new Guid("df32447c-7578-46af-a77a-73efd458c201"),
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

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c"),
                            EmailAddress = "ashkan@email.com",
                            UserName = "ashkan",
                            UserProperty = 160
                        },
                        new
                        {
                            UserId = new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7"),
                            EmailAddress = "jonas@email.com",
                            UserName = "jonas",
                            UserProperty = 1160
                        },
                        new
                        {
                            UserId = new Guid("43966394-6325-4e7d-a218-cf2d43faae24"),
                            EmailAddress = "amin@email.com",
                            UserName = "amin",
                            UserProperty = 10
                        });
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserSymbolProperty", b =>
                {
                    b.Property<Guid>("UserSymbolPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SymbolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SymbolPrice")
                        .HasColumnType("int");

                    b.Property<int>("SymbolQuantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserSymbolPropertyId");

                    b.HasIndex("SymbolId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSymbolProperty", (string)null);

                    b.HasData(
                        new
                        {
                            UserSymbolPropertyId = new Guid("2af3944d-8dfa-4262-a9e0-bb96ad2c7166"),
                            SymbolId = new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"),
                            SymbolPrice = 40,
                            SymbolQuantity = 170,
                            UserId = new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c")
                        },
                        new
                        {
                            UserSymbolPropertyId = new Guid("98818f76-e989-4be4-afb0-d61db4bbd590"),
                            SymbolId = new Guid("df32447c-7578-46af-a77a-73efd458c201"),
                            SymbolPrice = 450,
                            SymbolQuantity = 1370,
                            UserId = new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c")
                        },
                        new
                        {
                            UserSymbolPropertyId = new Guid("a7d97c83-0c10-45bf-94e7-05c43d59257a"),
                            SymbolId = new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"),
                            SymbolPrice = 450,
                            SymbolQuantity = 1370,
                            UserId = new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7")
                        },
                        new
                        {
                            UserSymbolPropertyId = new Guid("61b5469a-ee2e-4083-ab6a-975c8d2e4fe3"),
                            SymbolId = new Guid("df32447c-7578-46af-a77a-73efd458c201"),
                            SymbolPrice = 450,
                            SymbolQuantity = 1370,
                            UserId = new Guid("43966394-6325-4e7d-a218-cf2d43faae24")
                        });
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserWatchList", b =>
                {
                    b.Property<Guid>("UserWatchListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userWatchListName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserWatchListId");

                    b.HasIndex("UserId");

                    b.ToTable("UserWatchLists");

                    b.HasData(
                        new
                        {
                            UserWatchListId = new Guid("bc9e6ea8-f280-476e-8502-8d96926cde3e"),
                            UserId = new Guid("43966394-6325-4e7d-a218-cf2d43faae24"),
                            userWatchListName = "سهام من"
                        },
                        new
                        {
                            UserWatchListId = new Guid("dbb733b5-86e1-4b9b-b1d7-e9d26d77165d"),
                            UserId = new Guid("43966394-6325-4e7d-a218-cf2d43faae24"),
                            userWatchListName = "سهامppppp"
                        });
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserSymbolProperty", b =>
                {
                    b.HasOne("AspTechTrader.Core.Domain.Entities.Symbol", "Symbol")
                        .WithMany("UserSymbolProperties")
                        .HasForeignKey("SymbolId");

                    b.HasOne("AspTechTrader.Core.Domain.Entities.User", "User")
                        .WithMany("UserSymbolProperties")
                        .HasForeignKey("UserId");

                    b.Navigation("Symbol");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.UserWatchList", b =>
                {
                    b.HasOne("AspTechTrader.Core.Domain.Entities.User", "User")
                        .WithMany("UserWatchLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.Symbol", b =>
                {
                    b.Navigation("UserSymbolProperties");
                });

            modelBuilder.Entity("AspTechTrader.Core.Domain.Entities.User", b =>
                {
                    b.Navigation("UserSymbolProperties");

                    b.Navigation("UserWatchLists");
                });
#pragma warning restore 612, 618
        }
    }
}
