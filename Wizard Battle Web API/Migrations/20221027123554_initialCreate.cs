using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class initialCreate : Migration
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
                name: "Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    PlayerImage = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    PlayerStatus = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    ExperiencePoints = table.Column<long>(type: "bigint", nullable: false),
                    MaxHealth = table.Column<double>(type: "float", nullable: false),
                    MaxMana = table.Column<double>(type: "float", nullable: false),
                    KnowledgePoints = table.Column<long>(type: "bigint", nullable: false),
                    TimeCapsules = table.Column<long>(type: "bigint", nullable: false),
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
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => new { x.SenderID, x.ReceiverID });
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
                values: new object[] { 1, "test@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$iRuM/fMkLlZSPj9tKo4D1OYxitvmWyOLCZ9lsrL5zw7.fgk.OpPw6" });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Email", "Last_Login", "Modified_At", "Password" },
                values: new object[] { 2, "alex@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$10$UgOhCS6eT4w9PR6Q3tnO7OMlox3LjXVwJ5gnyOK/qz0WVX7h9Rix2" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerID", "AccountID", "ExperiencePoints", "KnowledgePoints", "MaxHealth", "MaxMana", "Modified_At", "PlayerImage", "PlayerName", "PlayerStatus", "TimeCapsules" },
                values: new object[] { 1, 1, 167L, 10L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "../../../../assets/alex.png", "NickTheG", "Online", 10L });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerID", "AccountID", "ExperiencePoints", "KnowledgePoints", "MaxHealth", "MaxMana", "Modified_At", "PlayerImage", "PlayerName", "PlayerStatus", "TimeCapsules" },
                values: new object[] { 2, 2, 138L, 10L, 10.0, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "../../../../assets/alex.png", "AlexTheG", "Away", 10L });

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
                name: "IX_Player_AccountID",
                table: "Player",
                column: "AccountID",
                unique: true);

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
                name: "Player");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
