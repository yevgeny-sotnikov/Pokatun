﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokatun.API.Models;

namespace Pokatun.API.Migrations
{
    [DbContext(typeof(PokatunContext))]
    partial class PokatunContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pokatun.API.Entities.Hotel", b =>
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

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(64)")
                        .HasMaxLength(64);

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("USREOU")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IBAN")
                        .IsUnique()
                        .HasFilter("[IBAN] IS NOT NULL");

                    b.HasIndex("ResetToken")
                        .IsUnique()
                        .HasFilter("[ResetToken] IS NOT NULL");

                    b.HasIndex("USREOU")
                        .IsUnique();

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Pokatun.API.Entities.Phone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("HotelId")
                        .HasColumnType("bigint");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Pokatun.API.Entities.SocialResource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("HotelId")
                        .HasColumnType("bigint");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("Link")
                        .IsUnique();

                    b.ToTable("SocialResources");
                });

            modelBuilder.Entity("Pokatun.API.Entities.Phone", b =>
                {
                    b.HasOne("Pokatun.API.Entities.Hotel", "Hotel")
                        .WithMany("Phones")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pokatun.API.Entities.SocialResource", b =>
                {
                    b.HasOne("Pokatun.API.Entities.Hotel", "Hotel")
                        .WithMany("SocialResources")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
