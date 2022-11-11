﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wizard_Battle_Web_API.Database;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Last_Login")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AccountID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            AccountID = 1,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "nick@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$2rxroRUTnzjlkIosh9nuz.eWS.GxpM5TGZB/556OJXbovZGAod0ZS"
                        },
                        new
                        {
                            AccountID = 2,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alex@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$dJXQGZUaU2xxpBOwatxPdubmHU8h4ZSWjHdIDrdWQDbgbMOp1Gt.i"
                        },
                        new
                        {
                            AccountID = 3,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mart@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$AEfc3iOE3g76JyPMEgzQWe1PWJKyN67nPK5tDKFFsfjeAtPZA/6UO"
                        },
                        new
                        {
                            AccountID = 4,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "marc@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$Kp1ZdAjVOH0yqRGBtq0er.grVFFcchEr1bw2Ipm.NaoPtJ9BAUuEG"
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Friendship", b =>
                {
                    b.Property<int>("MainPlayerID")
                        .HasColumnType("int");

                    b.Property<int>("FriendPlayerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("IsPending")
                        .HasColumnType("bit");

                    b.HasKey("MainPlayerID", "FriendPlayerID");

                    b.HasIndex("FriendPlayerID");

                    b.ToTable("Friendship");

                    b.HasData(
                        new
                        {
                            MainPlayerID = 1,
                            FriendPlayerID = 2,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2830),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 1,
                            FriendPlayerID = 3,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2842),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 1,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2846),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 2,
                            FriendPlayerID = 3,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2849),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 2,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2852),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 3,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 11, 7, 1, 13, 85, DateTimeKind.Utc).AddTicks(2919),
                            IsPending = false
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Icon", b =>
                {
                    b.Property<int>("IconID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IconID"), 1L, 1);

                    b.Property<string>("IconName")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IconID");

                    b.ToTable("Icon");

                    b.HasData(
                        new
                        {
                            IconID = 1,
                            IconName = "../../../../assets/player-icons/wizard1.png"
                        },
                        new
                        {
                            IconID = 2,
                            IconName = "../../../../assets/player-icons/wizard2.png"
                        },
                        new
                        {
                            IconID = 3,
                            IconName = "../../../../assets/player-icons/wizard3.png"
                        },
                        new
                        {
                            IconID = 4,
                            IconName = "../../../../assets/player-icons/wizard4.png"
                        },
                        new
                        {
                            IconID = 5,
                            IconName = "../../../../assets/player-icons/alex.png"
                        },
                        new
                        {
                            IconID = 6,
                            IconName = "../../../../assets/player-icons/alex-glasses.png"
                        },
                        new
                        {
                            IconID = 7,
                            IconName = "../../../../assets/player-icons/alex-mustache.png"
                        },
                        new
                        {
                            IconID = 8,
                            IconName = "../../../../assets/player-icons/alex-gangster.png"
                        },
                        new
                        {
                            IconID = 9,
                            IconName = "../../../../assets/player-icons/alex-impersonator.jpg"
                        },
                        new
                        {
                            IconID = 10,
                            IconName = "../../../../assets/player-icons/nick-gangster.png"
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageID"), 1L, 1);

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<int>("SenderID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MessageID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"), 1L, 1);

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<long>("ExperiencePoints")
                        .HasColumnType("bigint");

                    b.Property<int>("IconID")
                        .HasColumnType("int");

                    b.Property<long>("KnowledgePoints")
                        .HasColumnType("bigint");

                    b.Property<double>("MaxHealth")
                        .HasColumnType("float");

                    b.Property<double>("MaxMana")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerImage")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PlayerStatus")
                        .HasColumnType("nvarchar(32)");

                    b.Property<long>("TimeCapsules")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.HasIndex("IconID");

                    b.HasIndex("PlayerName")
                        .IsUnique()
                        .HasFilter("[PlayerName] IS NOT NULL");

                    b.ToTable("Player");

                    b.HasData(
                        new
                        {
                            PlayerID = 1,
                            AccountID = 1,
                            ExperiencePoints = 167L,
                            IconID = 1,
                            KnowledgePoints = 10L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "NickTheG",
                            PlayerStatus = "Offline",
                            TimeCapsules = 1000L
                        },
                        new
                        {
                            PlayerID = 2,
                            AccountID = 2,
                            ExperiencePoints = 138L,
                            IconID = 2,
                            KnowledgePoints = 10L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "AlexTheG",
                            PlayerStatus = "Offline",
                            TimeCapsules = 10L
                        },
                        new
                        {
                            PlayerID = 3,
                            AccountID = 3,
                            ExperiencePoints = 138L,
                            IconID = 3,
                            KnowledgePoints = 10L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "MartinTheG",
                            PlayerStatus = "Offline",
                            TimeCapsules = 10L
                        },
                        new
                        {
                            PlayerID = 4,
                            AccountID = 4,
                            ExperiencePoints = 138L,
                            IconID = 4,
                            KnowledgePoints = 10L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "MarcoTheG",
                            PlayerStatus = "Offline",
                            TimeCapsules = 10L
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

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.SkinItem", b =>
                {
                    b.Property<int>("SkinID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkinID"), 1L, 1);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SkinDescription")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SkinName")
                        .HasColumnType("nvarchar(60)");

                    b.Property<short>("SkinPrice")
                        .HasColumnType("smallint");

                    b.HasKey("SkinID");

                    b.ToTable("Skin");

                    b.HasData(
                        new
                        {
                            SkinID = 1,
                            ImageName = "../../../../assets/skin-images/wise-wizard.jpg",
                            SkinDescription = "A very wise wizard",
                            SkinName = "Wise Wizard",
                            SkinPrice = (short)125
                        },
                        new
                        {
                            SkinID = 2,
                            ImageName = "../../../../assets/skin-images/evil-wizard.jpg",
                            SkinDescription = "A very evil wizard",
                            SkinName = "Evil Wizard",
                            SkinPrice = (short)125
                        },
                        new
                        {
                            SkinID = 3,
                            ImageName = "../../../../assets/skin-images/suspicious-wizard.jpg",
                            SkinDescription = "A very suspicious wizard",
                            SkinName = "Suspicious Wizard",
                            SkinPrice = (short)125
                        },
                        new
                        {
                            SkinID = 4,
                            ImageName = "../../../../assets/skin-images/robot-wizard.jpg",
                            SkinDescription = "A very unhuman wizard",
                            SkinName = "Robot Wizard",
                            SkinPrice = (short)125
                        });
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionID"), 1L, 1);

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("SkinID")
                        .HasColumnType("int");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.HasKey("TransactionID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("SkinID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Friendship", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Player", "FriendPlayer")
                        .WithMany("Friends")
                        .HasForeignKey("FriendPlayerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Player", "MainPlayer")
                        .WithMany("MainPlayerFriends")
                        .HasForeignKey("MainPlayerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FriendPlayer");

                    b.Navigation("MainPlayer");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Message", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Player", "Receiver")
                        .WithMany("FriendMessages")
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Player", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Player", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Account", "Account")
                        .WithOne("Player")
                        .HasForeignKey("Wizard_Battle_Web_API.Database.Entities.Player", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Icon", "Icon")
                        .WithMany()
                        .HasForeignKey("IconID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.RefreshToken", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Account", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountID");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Transaction", b =>
                {
                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.Player", "Player")
                        .WithMany("Transactions")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wizard_Battle_Web_API.Database.Entities.SkinItem", "SkinItem")
                        .WithMany("Transactions")
                        .HasForeignKey("SkinID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("SkinItem");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Account", b =>
                {
                    b.Navigation("Player");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.Player", b =>
                {
                    b.Navigation("FriendMessages");

                    b.Navigation("Friends");

                    b.Navigation("MainPlayerFriends");

                    b.Navigation("Messages");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Wizard_Battle_Web_API.Database.Entities.SkinItem", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
