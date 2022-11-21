using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(64)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Login = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    IconID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconName = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.IconID);
                });

            migrationBuilder.CreateTable(
                name: "Skin",
                columns: table => new
                {
                    SkinID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkinName = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    SkinDescription = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    SkinPrice = table.Column<short>(type: "smallint", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skin", x => x.SkinID);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Expires_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedByIp = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    Revoked_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenID);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    IconID = table.Column<int>(type: "int", nullable: false),
                    PlayerStatus = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    ExperiencePoints = table.Column<long>(type: "bigint", nullable: false),
                    MaxHealth = table.Column<double>(type: "float", nullable: false),
                    MaxMana = table.Column<double>(type: "float", nullable: false),
                    KnowledgePoints = table.Column<long>(type: "bigint", nullable: false),
                    TimeCapsules = table.Column<long>(type: "bigint", nullable: false),
                    MatchWins = table.Column<long>(type: "bigint", nullable: false),
                    MatchLosses = table.Column<long>(type: "bigint", nullable: false),
                    TimePlayedMin = table.Column<long>(type: "bigint", nullable: false),
                    AvgDamage = table.Column<long>(type: "bigint", nullable: false),
                    AvgSpellsHit = table.Column<long>(type: "bigint", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_Player_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Icon_IconID",
                        column: x => x.IconID,
                        principalTable: "Icon",
                        principalColumn: "IconID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spell",
                columns: table => new
                {
                    SpellID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpellName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    SpellDescription = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    IconID = table.Column<int>(type: "int", nullable: false),
                    ManaCost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DamageAmount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    CastTime = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spell", x => x.SpellID);
                    table.ForeignKey(
                        name: "FK_Spell_Icon_IconID",
                        column: x => x.IconID,
                        principalTable: "Icon",
                        principalColumn: "IconID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    MainPlayerID = table.Column<int>(type: "int", nullable: false),
                    FriendPlayerID = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsPending = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => new { x.MainPlayerID, x.FriendPlayerID });
                    table.ForeignKey(
                        name: "FK_Friendship_Player_FriendPlayerID",
                        column: x => x.FriendPlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendship_Player_MainPlayerID",
                        column: x => x.MainPlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_Player_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Player_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpellBook",
                columns: table => new
                {
                    SpellBookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpellBookName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellBook", x => x.SpellBookID);
                    table.ForeignKey(
                        name: "FK_SpellBook_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkinID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transaction_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Skin_SkinID",
                        column: x => x.SkinID,
                        principalTable: "Skin",
                        principalColumn: "SkinID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpellBookSlot",
                columns: table => new
                {
                    SpellBookID = table.Column<int>(type: "int", nullable: false),
                    SpellID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellBookSlot", x => new { x.SpellID, x.SpellBookID });
                    table.ForeignKey(
                        name: "FK_SpellBookSlot_Spell_SpellID",
                        column: x => x.SpellID,
                        principalTable: "Spell",
                        principalColumn: "SpellID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpellBookSlot_SpellBook_SpellBookID",
                        column: x => x.SpellBookID,
                        principalTable: "SpellBook",
                        principalColumn: "SpellBookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Email", "Last_Login", "Modified_At", "Password" },
                values: new object[,]
                {
                    { 1, "nick@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$/JJYXRYXSbwA4ISELg5pLuWIRKJn206y6khaN942qPhcjFHiGy6TC" },
                    { 2, "alex@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$PAkQmuBv.dqks6WRoEEAyOvdygDGDNylGIAiVoH0FCsn5z8FKMAaW" },
                    { 3, "mart@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$Y9rZ6DKEToH694J4klu0A.J0jJd1HRFN99fUqmKUQPCSE.EuxMExK" },
                    { 4, "marc@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$.qvbsF.KC.f1XdC0TcJNB.CkzhxOSfTX8aKvpQ2IqZHzYCvJ0Ijt." }
                });

            migrationBuilder.InsertData(
                table: "Icon",
                columns: new[] { "IconID", "IconName" },
                values: new object[,]
                {
                    { 1, "../../../../assets/player-icons/wizard1.png" },
                    { 2, "../../../../assets/player-icons/wizard2.png" },
                    { 3, "../../../../assets/player-icons/wizard3.png" },
                    { 4, "../../../../assets/player-icons/wizard4.png" },
                    { 5, "../../../../assets/player-icons/alex.png" },
                    { 6, "../../../../assets/player-icons/alex-glasses.png" },
                    { 7, "../../../../assets/player-icons/alex-mustache.png" },
                    { 8, "../../../../assets/player-icons/alex-gangster.png" },
                    { 9, "../../../../assets/player-icons/alex-impersonator.jpg" },
                    { 10, "../../../../assets/player-icons/nick-gangster.png" },
                    { 11, "../../../../assets/spell-icons/fireball.png" }
                });

            migrationBuilder.InsertData(
                table: "Skin",
                columns: new[] { "SkinID", "ImageName", "SkinDescription", "SkinName", "SkinPrice" },
                values: new object[,]
                {
                    { 1, "../../../../assets/skin-images/wise-wizard.jpg", "A very wise wizard", "Wise Wizard", (short)125 },
                    { 2, "../../../../assets/skin-images/evil-wizard.jpg", "A very evil wizard", "Evil Wizard", (short)125 },
                    { 3, "../../../../assets/skin-images/suspicious-wizard.jpg", "A very suspicious wizard", "Suspicious Wizard", (short)125 },
                    { 4, "../../../../assets/skin-images/robot-wizard.jpg", "A very unhuman wizard", "Robot Wizard", (short)125 }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerID", "AccountID", "AvgDamage", "AvgSpellsHit", "ExperiencePoints", "IconID", "KnowledgePoints", "MatchLosses", "MatchWins", "MaxHealth", "MaxMana", "Modified_At", "PlayerName", "PlayerStatus", "TimeCapsules", "TimePlayedMin" },
                values: new object[,]
                {
                    { 1, 1, 150L, 13L, 167L, 1, 10L, 10L, 20L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NickTheG", "Offline", 1000L, 120L },
                    { 2, 2, 122L, 11L, 139L, 2, 10L, 7L, 12L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AlexTheG", "Offline", 10L, 75L },
                    { 3, 3, 133L, 12L, 138L, 3, 10L, 5L, 9L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MartinTheG", "Offline", 10L, 59L },
                    { 4, 4, 99L, 7L, 137L, 4, 10L, 7L, 4L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarcoTheG", "Offline", 10L, 43L }
                });

            migrationBuilder.InsertData(
                table: "Spell",
                columns: new[] { "SpellID", "CastTime", "DamageAmount", "IconID", "ManaCost", "SpellDescription", "SpellName" },
                values: new object[,]
                {
                    { 1, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 1" },
                    { 2, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 2" },
                    { 3, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 3" },
                    { 4, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 4" },
                    { 5, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 5" },
                    { 6, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 6" },
                    { 7, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 7" },
                    { 8, 0m, 0m, 11, 0m, "It's a fireball, does it really need a description?", "Fireball 8" }
                });

            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "FriendPlayerID", "MainPlayerID", "Created_At", "IsPending" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8050), false },
                    { 3, 1, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8061), false },
                    { 4, 1, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8069), false },
                    { 3, 2, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8075), false },
                    { 4, 2, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8082), false },
                    { 4, 3, new DateTime(2022, 11, 21, 11, 32, 34, 609, DateTimeKind.Utc).AddTicks(8090), false }
                });

            migrationBuilder.InsertData(
                table: "SpellBook",
                columns: new[] { "SpellBookID", "PlayerID", "SpellBookName" },
                values: new object[] { 1, 1, "My SpellBook" });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionID", "PlayerID", "SkinID", "TotalCost" },
                values: new object[] { 1, 1, 1, 125 });

            migrationBuilder.InsertData(
                table: "SpellBookSlot",
                columns: new[] { "SpellBookID", "SpellID" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                table: "Account",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendPlayerID",
                table: "Friendship",
                column: "FriendPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverID",
                table: "Message",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderID",
                table: "Message",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_AccountID",
                table: "Player",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_IconID",
                table: "Player",
                column: "IconID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerName",
                table: "Player",
                column: "PlayerName",
                unique: true,
                filter: "[PlayerName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountID",
                table: "RefreshToken",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_IconID",
                table: "Spell",
                column: "IconID");

            migrationBuilder.CreateIndex(
                name: "IX_SpellBook_PlayerID",
                table: "SpellBook",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_SpellBookSlot_SpellBookID",
                table: "SpellBookSlot",
                column: "SpellBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PlayerID",
                table: "Transaction",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SkinID",
                table: "Transaction",
                column: "SkinID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SpellBookSlot");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Spell");

            migrationBuilder.DropTable(
                name: "SpellBook");

            migrationBuilder.DropTable(
                name: "Skin");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Icon");
        }
    }
}
