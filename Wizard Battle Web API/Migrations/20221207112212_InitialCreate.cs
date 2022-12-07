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
                name: "SpellSchool",
                columns: table => new
                {
                    SpellSchoolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpellSchoolName = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSchool", x => x.SpellSchoolID);
                });

            migrationBuilder.CreateTable(
                name: "SpellType",
                columns: table => new
                {
                    SpellTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpellTypeName = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellType", x => x.SpellTypeID);
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
                    SpellBookID = table.Column<long>(type: "bigint", nullable: false),
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
                name: "SchoolCategory",
                columns: table => new
                {
                    SchoolCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolCategoryName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    SpellSchoolID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCategory", x => x.SchoolCategoryID);
                    table.ForeignKey(
                        name: "FK_SchoolCategory_SpellSchool_SpellSchoolID",
                        column: x => x.SpellSchoolID,
                        principalTable: "SpellSchool",
                        principalColumn: "SpellSchoolID",
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
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    SpellOrder = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Spell",
                columns: table => new
                {
                    SpellID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpellName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    SpellDescription = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    IconID = table.Column<int>(type: "int", nullable: false),
                    SpellTypeID = table.Column<int>(type: "int", nullable: false),
                    SchoolCategoryID = table.Column<int>(type: "int", nullable: false),
                    DamageAmount = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    ManaCost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    LifeTime = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Spell_SchoolCategory_SchoolCategoryID",
                        column: x => x.SchoolCategoryID,
                        principalTable: "SchoolCategory",
                        principalColumn: "SchoolCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spell_SpellType_SpellTypeID",
                        column: x => x.SpellTypeID,
                        principalTable: "SpellType",
                        principalColumn: "SpellTypeID",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, "nick@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$2MUeXglTnA1sqnmoL7urQeC4vs7kITVdG7tP3uhKLAebbVYaFajIy" },
                    { 2, "alex@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$WarfaMuR6p6uN3yzGhlVLuRcu/mOfjzj7cSOjQV7PsV6LuPAKf.US" },
                    { 3, "mart@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$uCwhgFTesJiL0OW4lq6NNeFPgk4PHcdTftvcJO28TB1Lb90M3NhHm" },
                    { 4, "marc@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$YwH6mtzojgqhBmN0UughUOqNefgHUPtTht.8RSa8NtntfZdokqQdW" }
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
                    { 11, "../../../../assets/spell-icons/fireball.png" },
                    { 12, "../../../../assets/spell-icons/firenova.png" },
                    { 13, "../../../../assets/spell-icons/firewall.png" },
                    { 14, "../../../../assets/spell-icons/teleport.png" },
                    { 15, "../../../../assets/spell-icons/windslash.png" },
                    { 16, "../../../../assets/spell-icons/watervortex.png" },
                    { 17, "../../../../assets/spell-icons/dash.png" },
                    { 18, "../../../../assets/spell-icons/placeholder-spell-icon-1.png" },
                    { 19, "../../../../assets/spell-icons/placeholder-spell-icon-2.png" },
                    { 20, "../../../../assets/spell-icons/placeholder-spell-icon-3.png" },
                    { 21, "../../../../assets/spell-icons/placeholder-spell-icon-4.png" },
                    { 22, "../../../../assets/spell-icons/placeholder-spell-icon-5.png" },
                    { 23, "../../../../assets/spell-icons/placeholder-spell-icon-6.png" },
                    { 24, "../../../../assets/spell-icons/placeholder-spell-icon-7.png" },
                    { 25, "../../../../assets/spell-icons/placeholder-spell-icon-8.png" },
                    { 26, "../../../../assets/spell-icons/rockspear.png" },
                    { 27, "../../../../assets/spell-icons/invisible.png" }
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
                table: "SpellSchool",
                columns: new[] { "SpellSchoolID", "SpellSchoolName" },
                values: new object[,]
                {
                    { 1, "Elemental" },
                    { 2, "Primal" },
                    { 3, "Void" },
                    { 4, "Ether" }
                });

            migrationBuilder.InsertData(
                table: "SpellType",
                columns: new[] { "SpellTypeID", "SpellTypeName" },
                values: new object[,]
                {
                    { 1, "Offensive" },
                    { 2, "Defensive" },
                    { 3, "Utility" }
                });

            migrationBuilder.InsertData(
                table: "SpellType",
                columns: new[] { "SpellTypeID", "SpellTypeName" },
                values: new object[] { 4, "Ultimate" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerID", "AccountID", "AvgDamage", "AvgSpellsHit", "ExperiencePoints", "IconID", "KnowledgePoints", "MatchLosses", "MatchWins", "MaxHealth", "MaxMana", "Modified_At", "PlayerName", "PlayerStatus", "SpellBookID", "TimeCapsules", "TimePlayedMin" },
                values: new object[,]
                {
                    { 1, 1, 150L, 13L, 167L, 1, 10L, 10L, 20L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NickTheG", "Offline", 1L, 1000L, 120L },
                    { 2, 2, 122L, 11L, 139L, 2, 10L, 7L, 12L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AlexTheG", "Offline", 4L, 10L, 75L },
                    { 3, 3, 133L, 12L, 138L, 3, 10L, 5L, 9L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MartinTheG", "Offline", 7L, 10L, 59L },
                    { 4, 4, 99L, 7L, 137L, 4, 10L, 7L, 4L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarcoTheG", "Offline", 10L, 10L, 43L }
                });

            migrationBuilder.InsertData(
                table: "SchoolCategory",
                columns: new[] { "SchoolCategoryID", "SchoolCategoryName", "SpellSchoolID" },
                values: new object[,]
                {
                    { 1, "Fire", 1 },
                    { 2, "Water", 1 },
                    { 3, "Earth", 1 },
                    { 4, "Air", 1 },
                    { 5, "Arcane", 2 },
                    { 6, "Dimensional", 3 },
                    { 7, "Dark", 3 },
                    { 8, "Light", 4 },
                    { 9, "Spirit", 4 }
                });

            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "FriendPlayerID", "MainPlayerID", "Created_At", "IsPending" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7626), false },
                    { 3, 1, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7629), false },
                    { 4, 1, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7630), false },
                    { 3, 2, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7631), false },
                    { 4, 2, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7631), false },
                    { 4, 3, new DateTime(2022, 12, 7, 11, 22, 12, 230, DateTimeKind.Utc).AddTicks(7632), false }
                });

            migrationBuilder.InsertData(
                table: "Spell",
                columns: new[] { "SpellID", "CastTime", "DamageAmount", "IconID", "LifeTime", "ManaCost", "SchoolCategoryID", "SpellDescription", "SpellName", "SpellTypeID" },
                values: new object[,]
                {
                    { 1, 0.8m, 15m, 11, 4m, 10m, 1, "A ball of fire!", "Fireball", 1 },
                    { 2, 2m, 18m, 12, 3m, 18m, 1, "Explodes a ring of fire around the wizard", "Fire Nova", 1 },
                    { 3, 0.15m, 75m, 13, 8m, 30m, 1, "A wall of fire!", "Fire Wall", 1 },
                    { 4, 0.5m, 16m, 16, 0.8m, 12m, 2, "A water vortex is created at the target location.", "Water Vortex", 1 },
                    { 5, 0.75m, 12m, 26, 4m, 10m, 3, "Throw a spear made of solid rock that stuns on impact.", "Rock Spear", 1 },
                    { 6, 0.2m, 6m, 15, 2m, 5m, 4, "Send out a slash of wind that damages enemies and speeds up the caster.", "Wind Slash", 1 },
                    { 7, 0.1m, 0m, 17, 1m, 10m, 5, "Dash a short distance.", "Dash", 3 },
                    { 8, 0.75m, 0m, 27, 1m, 25m, 9, "Makes you invisible to the naked eye.", "Invisible", 1 },
                    { 9, 0.5m, 0m, 14, 1m, 20m, 5, "Teleport to a location instantly.", "Teleport", 1 }
                });

            migrationBuilder.InsertData(
                table: "SpellBook",
                columns: new[] { "SpellBookID", "PlayerID", "SpellBookName", "SpellOrder" },
                values: new object[,]
                {
                    { 1, 1, "Unnamed Spellbook 1", null },
                    { 2, 1, "Unnamed Spellbook 2", null },
                    { 3, 1, "Unnamed Spellbook 3", null },
                    { 4, 2, "Unnamed Spellbook 1", null },
                    { 5, 2, "Unnamed Spellbook 2", null },
                    { 6, 2, "Unnamed Spellbook 3", null },
                    { 7, 3, "Unnamed Spellbook 1", null },
                    { 8, 3, "Unnamed Spellbook 2", null },
                    { 9, 3, "Unnamed Spellbook 3", null },
                    { 10, 4, "Unnamed Spellbook 1", null },
                    { 11, 4, "Unnamed Spellbook 2", null },
                    { 12, 4, "Unnamed Spellbook 3", null }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionID", "PlayerID", "SkinID", "TotalCost" },
                values: new object[] { 1, 1, 1, 125 });

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
                name: "IX_SchoolCategory_SpellSchoolID",
                table: "SchoolCategory",
                column: "SpellSchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_IconID",
                table: "Spell",
                column: "IconID");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_SchoolCategoryID",
                table: "Spell",
                column: "SchoolCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_SpellName",
                table: "Spell",
                column: "SpellName",
                unique: true,
                filter: "[SpellName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Spell_SpellTypeID",
                table: "Spell",
                column: "SpellTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SpellBook_PlayerID",
                table: "SpellBook",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_SpellBookSlot_SpellBookID",
                table: "SpellBookSlot",
                column: "SpellBookID");

            migrationBuilder.CreateIndex(
                name: "IX_SpellType_SpellTypeName",
                table: "SpellType",
                column: "SpellTypeName",
                unique: true,
                filter: "[SpellTypeName] IS NOT NULL");

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
                name: "SchoolCategory");

            migrationBuilder.DropTable(
                name: "SpellType");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "SpellSchool");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Icon");
        }
    }
}
