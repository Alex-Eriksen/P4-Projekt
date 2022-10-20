﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wizard_Battle_Web_API.Database;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221020114727_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"), 1L, 1);

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AccountID");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            AccountID = 1,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "test@test.com",
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$SOKYUyfQoFElCv9QOWoT0.w6uasMrFa8BZaEAEir5vZ/Iso2.teIe"
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"), 1L, 1);

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("PlayerID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.ToTable("Player");

                    b.HasData(
                        new
                        {
                            PlayerID = 1,
                            AccountID = 1,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "NickTheG"
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefreshTokenID"), 1L, 1);

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("Expires_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime?>("Revoked_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("RefreshTokenID");

                    b.HasIndex("AccountID");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Player", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Account", "Account")
                        .WithOne("Player")
                        .HasForeignKey("Wizard_Battle_Web_API.Database.Entities.Player", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.RefreshToken", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Account", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountID");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Account", b =>
                {
                    b.Navigation("Player");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
