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
                            Password = "$2a$10$tadSQD3gppiTUSxSSoM1W.FeMaIUiOUAkyMiU7yXbPNT2XTlDr.oy"
                        },
                        new
                        {
                            AccountID = 2,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alex@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$KZGymi3DejnQ.u3GxybqD.RcPWBnnWiTdnRF.rHxe5a5aVo9DzNgW"
                        },
                        new
                        {
                            AccountID = 3,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mart@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$nV3H.BRFU0I5jJG4HQmAqueC164HgYGXSypKTS0d6qyl1J1q3.eO6"
                        },
                        new
                        {
                            AccountID = 4,
                            Created_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "marc@test.com",
                            Last_Login = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$10$3Gx7k3Iwpe2GBqxrTyeJse7M1rKD6vOR3f9t1/KjzvOhBX2VKBqk2"
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
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6064),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 1,
                            FriendPlayerID = 3,
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6080),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 1,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6088),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 2,
                            FriendPlayerID = 3,
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6095),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 2,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6102),
                            IsPending = false
                        },
                        new
                        {
                            MainPlayerID = 3,
                            FriendPlayerID = 4,
                            Created_At = new DateTime(2022, 11, 4, 9, 42, 11, 582, DateTimeKind.Utc).AddTicks(6110),
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

                    b.Property<long>("MatchLosses")
                        .HasColumnType("bigint");

                    b.Property<long>("MatchWins")
                        .HasColumnType("bigint");

                    b.Property<double>("MaxHealth")
                        .HasColumnType("float");

                    b.Property<double>("MaxMana")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PlayerStatus")
                        .HasColumnType("nvarchar(32)");

                    b.Property<long>("TimeCapsules")
                        .HasColumnType("bigint");

                    b.Property<long>("TimePlayedMin")
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
                            MatchLosses = 0L,
                            MatchWins = 0L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "NickTheG",
                            PlayerStatus = "Online",
                            TimeCapsules = 10L,
                            TimePlayedMin = 0L
                        },
                        new
                        {
                            PlayerID = 2,
                            AccountID = 2,
                            ExperiencePoints = 138L,
                            IconID = 1,
                            KnowledgePoints = 10L,
                            MatchLosses = 0L,
                            MatchWins = 0L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "AlexTheG",
                            PlayerStatus = "Online",
                            TimeCapsules = 10L,
                            TimePlayedMin = 0L
                        },
                        new
                        {
                            PlayerID = 3,
                            AccountID = 3,
                            ExperiencePoints = 138L,
                            IconID = 1,
                            KnowledgePoints = 10L,
                            MatchLosses = 0L,
                            MatchWins = 0L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "MartinTheG",
                            PlayerStatus = "Online",
                            TimeCapsules = 10L,
                            TimePlayedMin = 0L
                        },
                        new
                        {
                            PlayerID = 4,
                            AccountID = 4,
                            ExperiencePoints = 138L,
                            IconID = 1,
                            KnowledgePoints = 10L,
                            MatchLosses = 0L,
                            MatchWins = 0L,
                            MaxHealth = 10.0,
                            MaxMana = 10.0,
                            Modified_At = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PlayerName = "MarcoTheG",
                            PlayerStatus = "Online",
                            TimeCapsules = 10L,
                            TimePlayedMin = 0L
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

                    b.HasKey("SkinID");

                    b.ToTable("Skin");
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
                });
#pragma warning restore 612, 618
        }
    }
}
