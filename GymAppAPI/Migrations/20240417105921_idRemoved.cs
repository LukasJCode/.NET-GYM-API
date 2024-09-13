using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class idRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6aa4026b-e1ee-460d-bea6-d56a58ca3d16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb9f4cd3-187c-4511-8b5b-3064cbcc655c");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserWorkouts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cff86df-bb0a-483f-aa24-70a0c0ad1c7f", null, "User", "USER" },
                    { "e512b9b1-b224-40d0-949d-1da936baf4a2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cff86df-bb0a-483f-aa24-70a0c0ad1c7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e512b9b1-b224-40d0-949d-1da936baf4a2");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6aa4026b-e1ee-460d-bea6-d56a58ca3d16", null, "User", "USER" },
                    { "cb9f4cd3-187c-4511-8b5b-3064cbcc655c", null, "Admin", "ADMIN" }
                });
        }
    }
}
