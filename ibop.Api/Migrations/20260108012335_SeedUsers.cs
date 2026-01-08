using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ibop.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("010efa23-1c78-4fdb-b40f-a1dde436ec70"), new DateTime(2026, 1, 8, 1, 23, 34, 509, DateTimeKind.Utc).AddTicks(9526), "staff@ibop.com", "Staff", true, "User", "AQAAAAIAAYagAAAAEFnyf5FrHyc99YDTItZa1Wm/JQ/vsKNXh/O5btB5nvzE/t+9JtzmuGLEjIKLTiNLbg==", 2 },
                    { new Guid("252d9f88-7d28-4b90-80fe-6839baaa2722"), new DateTime(2026, 1, 8, 1, 23, 34, 438, DateTimeKind.Utc).AddTicks(4356), "manager@ibop.com", "Manager", true, "User", "AQAAAAIAAYagAAAAEC5Su+fB1vyE4ar9Iy42dFH/tjQQ80cbbyJmQdqWMrRYv0hrC8XkylI+I/AC9VEUvw==", 1 },
                    { new Guid("680645ce-8113-4c18-80f3-280b360660e8"), new DateTime(2026, 1, 8, 1, 23, 34, 353, DateTimeKind.Utc).AddTicks(1364), "admin@ibop.com", "Admin", true, "User", "AQAAAAIAAYagAAAAEDpdvmeQzPk5OLaDSSu50a1kq3D7wvZuPZrwibwh894aF3uEv8T/P0gd8A63j5yBZg==", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("010efa23-1c78-4fdb-b40f-a1dde436ec70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("252d9f88-7d28-4b90-80fe-6839baaa2722"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("680645ce-8113-4c18-80f3-280b360660e8"));
        }
    }
}
