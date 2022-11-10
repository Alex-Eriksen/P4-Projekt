using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class initialcreate : Migration
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

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Email", "Last_Login", "Modified_At", "Password" },
                values: new object[,]
                {
                    { 1, "nick@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$iZXrLpt.VHeVKvAYc9vCLeoyAM1gGDsMQhdbxywZz.VnhkpAUEliG" },
                    { 2, "alex@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$8f6fRaByDhLLuHo5GqE8I.EUgkAOCL94P/iij1pBRGQKKTLccvWfW" },
                    { 3, "mart@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$Z7ZT4biJH.Yi0MQ6BH62jeX4fm/sCsoiORdKTSRIXxwTQluNLp.O." },
                    { 4, "marc@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$iRAMSSVkVwpkK/QYwIC0ju/n2LFupCgRAwFVfX43XTjXvZCHY/Ey2" }
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
                    { 10, "../../../../assets/player-icons/nick-gangster.png" }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerID", "AccountID", "ExperiencePoints", "IconID", "KnowledgePoints", "MatchLosses", "MatchWins", "MaxHealth", "MaxMana", "Modified_At", "PlayerName", "PlayerStatus", "TimeCapsules", "TimePlayedMin" },
                values: new object[,]
                {
                    { 1, 1, 167L, 1, 10L, 0L, 0L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NickTheG", "Online", 10L, 0L },
                    { 2, 2, 138L, 1, 10L, 0L, 0L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AlexTheG", "Online", 10L, 0L },
                    { 3, 3, 138L, 1, 10L, 0L, 0L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MartinTheG", "Online", 10L, 0L },
                    { 4, 4, 138L, 1, 10L, 0L, 0L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarcoTheG", "Online", 10L, 0L }
                });

            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "FriendPlayerID", "MainPlayerID", "Created_At", "IsPending" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1188), false },
                    { 3, 1, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1234), false },
                    { 4, 1, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1241), false },
                    { 3, 2, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1248), false },
                    { 4, 2, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1255), false },
                    { 4, 3, new DateTime(2022, 11, 9, 8, 59, 7, 867, DateTimeKind.Utc).AddTicks(1263), false }
                });

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
