﻿// <auto-generated />
using System;
using DemoProject.DLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DemoProject.DLL.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20180923210337_Add-ChangeHistory-Table")]
    partial class AddChangeHistoryTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DemoProject.DLL.Models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.CartShopItem", b =>
                {
                    b.Property<Guid>("CartId");

                    b.Property<Guid>("ShopItemId");

                    b.Property<int>("Count");

                    b.Property<decimal>("Price");

                    b.HasKey("CartId", "ShopItemId");

                    b.HasIndex("ShopItemId");

                    b.ToTable("CartShopItems");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.ChangeHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TableName")
                        .IsRequired();

                    b.Property<DateTime>("TimeOfChange");

                    b.HasKey("Id");

                    b.ToTable("History");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.InfoObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<Guid>("DiscountId");

                    b.Property<int>("SubOrder");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.ToTable("InfoObjects");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Icon")
                        .IsRequired()
                        .HasColumnType("image");

                    b.Property<string>("IconContentType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<Guid>("CartId");

                    b.Property<string>("Comment");

                    b.Property<DateTime?>("DateOfApproved");

                    b.Property<DateTime?>("DateOfClosed");

                    b.Property<DateTime>("DateOfCreation")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("DateOfRejected");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.ShopItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("image");

                    b.Property<string>("ImageContentType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("MenuItemId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("ShopItems");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.ShopItemDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Price");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("ShopItemId");

                    b.HasKey("Id");

                    b.HasIndex("ShopItemId");

                    b.ToTable("ShopItemDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DemoProject.DLL.Models.CartShopItem", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.Cart", "Cart")
                        .WithMany("CartShopItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DemoProject.DLL.Models.ShopItem", "ShopItem")
                        .WithMany("CartShopItems")
                        .HasForeignKey("ShopItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoProject.DLL.Models.InfoObject", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.Discount", "Discount")
                        .WithMany("Items")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoProject.DLL.Models.Order", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoProject.DLL.Models.ShopItem", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.MenuItem", "MenuItem")
                        .WithMany("Items")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoProject.DLL.Models.ShopItemDetail", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.ShopItem", "ShopItem")
                        .WithMany("Details")
                        .HasForeignKey("ShopItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DemoProject.DLL.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DemoProject.DLL.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
