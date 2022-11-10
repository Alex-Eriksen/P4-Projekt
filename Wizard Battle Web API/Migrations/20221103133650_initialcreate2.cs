using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wizard_Battle_Web_API.Migrations
{
    public partial class initialcreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 1,
                column: "Password",
                value: "$2a$10$nq6j8z/Jd.vQ./bpU1GujOMtt80aYWq8DDvQjKQqIsUS8FghV01lq");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 2,
                column: "Password",
                value: "$2a$10$7gC6UTdnaBvOH4.2wBFmteVigkTwUup82uNtZbDB6OBYQA0UbmMCe");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 3,
                column: "Password",
                value: "$2a$10$t66m17LnXDLZgf9y4qFw0O0lWm9xFEwQNQvWbfPiNxLegiqv6dr/W");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountID",
                keyValue: 4,
                column: "Password",
                value: "$2a$10$jRoaAU.AgprH1F5X/Ik.0uuGJP9QMAhsUu7Mv77vvS18n0jcej9EK");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 2, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9569));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 1 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 3, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 2 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumns: new[] { "FriendPlayerID", "MainPlayerID" },
                keyValues: new object[] { 4, 3 },
                column: "Created_At",
                value: new DateTime(2022, 11, 3, 13, 32, 14, 79, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 1,
                column: "IconLocation",
                value: "../../../../assets/profile 1.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 2,
                column: "IconLocation",
                value: "../../../../assets/profile 2.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 3,
                column: "IconLocation",
                value: "../../../../assets/profile 3.png");

            migrationBuilder.UpdateData(
                table: "Icon",
                keyColumn: "IconID",
                keyValue: 4,
                column: "IconLocation",
                value: "../../../../assets/profile 4.png");
        }
    }
}
