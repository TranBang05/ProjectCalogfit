﻿// <auto-generated />
using System;
using Menu.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Menu.DataAccess.Migrations
{
    [DbContext(typeof(MenuDbContext))]
    [Migration("20240721080110_menuCreat")]
    partial class menuCreat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Menu.DataAccess.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealId"), 1L, 1);

                    b.Property<string>("MealTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.HasKey("MealId");

                    b.HasIndex("MenuId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("MenuTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"), 1L, 1);

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("MenuItemId");

                    b.HasIndex("MealId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.Meal", b =>
                {
                    b.HasOne("Menu.DataAccess.Models.Menu", "Menu")
                        .WithMany("Meals")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.MenuItem", b =>
                {
                    b.HasOne("Menu.DataAccess.Models.Meal", "Meal")
                        .WithMany("MenuItems")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.Meal", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("Menu.DataAccess.Models.Menu", b =>
                {
                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
