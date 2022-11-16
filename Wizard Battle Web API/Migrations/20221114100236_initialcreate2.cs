using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class initialcreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerImage",
                table: "Player");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$.dkmW.emBoV4xNIUXKAkBuTTfDMLvoys4tpyRD/4kdrl1o.m/SSxS");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$mGJZb6Ytxpj3EF/A5e4SZOU8ABg0aJAXJ8flbwdzzpAvWzqRD8.QO");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$uOoUh5QhdZym1KHfLe4LmeaTf/rCZRhQOB/WmyPqnqw8gsH3YzvhK");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$Mr5OnI/NOzXx1E2Dov0bx.f0Pe7O1dysS.xe7nhJpnMPPX4fa0lSO");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 2, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(478));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(485));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(492));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 3 },
                column: "Created_At",
                value: new DateTime(2022, 11, 14, 10, 2, 36, 73, DateTimeKind.Utc).AddTicks(499));

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionID", "PlayerID", "SkinID", "TotalCost" },
                values: new object[] { 1, 1, 1, 125 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionID",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "PlayerImage",
                table: "Player",
                type: "nvarchar(32)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$DFr.G62cHIdZAbI7IF51NeB5Lw8QuoBNf89saiRFxL4xDGTUqFD/q");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$YAN.5lMdrMTYQwip.rwyJerpFodnOi5SG3RvEs5rU5wD1UpbtrYBe");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$Q83HXaJvYBqgmrasCOv59.hWfXkbwsMPaam4mUkqQiRYTcXFTLSsm");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$eXtmwzbYMgMIt7k9rqTPrO0lkSnHUUri5Uvlvezz0ZqZEG2ILvrCS");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 2, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2491));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 3 },
                column: "Created_At",
                value: new DateTime(2022, 11, 11, 8, 54, 32, 809, DateTimeKind.Utc).AddTicks(2514));
        }
    }
}
