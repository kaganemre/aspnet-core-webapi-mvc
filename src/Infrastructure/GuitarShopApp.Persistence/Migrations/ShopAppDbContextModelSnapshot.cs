﻿// <auto-generated />
using System;
using GuitarShopApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuitarShopApp.Persistence.Migrations
{
    [DbContext(typeof(ShopAppDbContext))]
    partial class ShopAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Guitar",
                            Url = "guitar"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Amplifier",
                            Url = "amplifier"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pedal",
                            Url = "pedal"
                        });
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHome")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Harkening back to the early '90s when import Jackson® guitars were manufactured exclusively in Japan, we introduce the all-new Jackson MJ Series - an exciting and innovative collection of instruments attentively crafted in Japan. The MJ Series blends Jackson's world-renowned legacy of designing high-performance instruments with an assortment of top-tier features at a competitive price point.",
                            Image = "1.png",
                            IsHome = false,
                            Name = "Jackson RR24",
                            Price = 4500.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "The Dean Dimebag CHF Electric Guitar puts Dime's favorite body design behind a stunning graphic. The sleek, set mahogany neck with pau ferro fretboard is designed for speed, while the Seymour Duncan SH13 Dimebucker and  DMT Design neck humbuckers deliver all the high-output sonics you'll need. It also features dot inlays, classic V headstock shape, 24-3/4' scale, and Grover tuners. The Floyd Rose Special bridge will keep you in fine dive bombing form.",
                            Image = "4.png",
                            IsHome = true,
                            Name = "Dean Dimebag CHF",
                            Price = 3000.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Gibson and Slash are proud to present the Slash Collection Gibson Les Paul™ Standard. It represents influential Gibson guitars Slash has used during his career, inspiring multiple generations of players around the world. The Slash Collection of Gibson guitars can be seen live on stage with Slash today.",
                            Image = "2.png",
                            IsHome = true,
                            Name = "Gibson Slash Les Paul",
                            Price = 6000.0
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Created by hand, one at a time by the artisans of the ESP Custom Shop in Japan, the ESP Alexi Hexed is the identical model designed and played by one of the most beloved and influential figures in metal music: Alexi Laiho of Children of Bodom/Bodom After Midnight. The ESP Alexi Hexed is offered in an offset V shape with neck-thru-body construction at 25.5” scale.",
                            Image = "3.png",
                            IsHome = true,
                            Name = "ESP Alexi Hexed",
                            Price = 4000.0
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            Description = "Guitar virtuoso Jake E Lee blazed trails in the 1980s with Ratt and Rough Cutt before landing his legendary gig as Ozzy Osbourne's lead guitarist. His acclaimed career continued with Badlands and now Red Dragon Cartel, and Charvel has been there every step of the way. Charvel honors the dynamic guitarist with the new Jake E Lee Signature Pro-Mod So-Cal, based on the distinctive white 'Charvel-ized' instrument he acquired back in 1975.",
                            Image = "5.png",
                            IsHome = false,
                            Name = "Charvel HSS HT RW",
                            Price = 5000.0
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "The Marshall MG30GFX Gold 30W Guitar Combo features the iconic 'gold' front panel design and delivers 30 watts of portable Marshall tone with added sound effects and reverb. It is the ideal amplifier for band rehearsals and for small/medium gigs, with some added features making it perfect for practice.",
                            Image = "6.png",
                            IsHome = false,
                            Name = "Marshall MG30GFX 30W",
                            Price = 1500.0
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 3,
                            Description = "The Boss TR-2 Tremolo Guitar effects pedal delivers a vintage-style tremolo that can be fully controlled thanks to its intuitive controls. A designated Wave, Rate, and depth dial ensure every parameter can be intuitively altered to the performer's preference",
                            Image = "7.png",
                            IsHome = false,
                            Name = "Boss TR-2 Tremolo Pedal",
                            Price = 1000.0
                        });
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("GuitarShopApp.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuitarShopApp.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Product", b =>
                {
                    b.HasOne("GuitarShopApp.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuitarShopApp.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
