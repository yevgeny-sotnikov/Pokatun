﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokatun.API.Models;

namespace Pokatun.API.Migrations
{
    [DbContext(typeof(PokatunContext))]
    [Migration("20201030183447_PluralizationFix")]
    partial class PluralizationFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pokatun.Data.Hotel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BankCard")
                        .HasColumnType("bigint");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("FullCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("IBAN")
                        .HasColumnType("nvarchar(34)")
                        .HasMaxLength(34);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<int>("USREOU")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
