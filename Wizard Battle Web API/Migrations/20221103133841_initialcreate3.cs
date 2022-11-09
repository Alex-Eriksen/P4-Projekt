using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class initialcreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$lvlx9aanrb/Yg.7cnmk.Lux2sIIRgvJEQNsNK7UOK/1nQUnltEddC");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$Q389nQEBNpBcrF5qEfznxu0YiUU3rEclKYZppmTBXzbpuk5PaZVtK");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$s/XbkYhdve9wWavRUeTj7.meNnWUJZw9GfMO2QY9feeN46z6pL8UW");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$JEIVMxUaEwHac7zT3JwN/.ns1k0U6uyIE6J1GSVUVfIdP3Fo6LYDG");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 2, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 3 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 38, 40, 829, DateTimeKind.Utc).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 1,
                column: "IconLocation",
                value: "../../../../assets/player-icons/wizard1.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 2,
                column: "IconLocation",
                value: "../../../../assets/player-icons/wizard2.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 3,
                column: "IconLocation",
                value: "../../../../assets/player-icons/wizard3.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 4,
                column: "IconLocation",
                value: "../../../../assets/player-icons/wizard4.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 5,
                column: "IconLocation",
                value: "../../../../assets/player-icons/alex.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$ppXJU/J/N8toBx2pOiOdperkmeZeynCAqO/WmYQVogUgPAoIgDaFO");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$OwCopQPn72vsek6akwzPXeAfivmB25vrLAeWQsMjTR/Gobrj8GjqS");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$pMf8S4axWOUIA0P3ai4Cw.GWOdEw5A2J2DZQvAnsBwEtLT2K7f1LO");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$oGcrMbs59g0j13qlBQRDhO3PXsFMzYLfC.Ud0t2/q5Z9xVmeXHtAe");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 2, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7644));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7644));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 3 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 36, 50, 493, DateTimeKind.Utc).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 1,
                column: "IconLocation",
                value: "../../../../assets/profile-icons/wizard1.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 2,
                column: "IconLocation",
                value: "../../../../assets/profile-icons/wizard2.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 3,
                column: "IconLocation",
                value: "../../../../assets/profile-icons/wizard3.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 4,
                column: "IconLocation",
                value: "../../../../assets/profile-icons/wizard4.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 5,
                column: "IconLocation",
                value: "../../../../assets/alex.png");
        }
    }
}
