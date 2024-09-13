using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserWorkoutIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06343ddc-e8f3-43d4-9b9a-d0613d310768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8c754e8-f0ec-4798-bc2a-a088a98ba3d6");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "06343ddc-e8f3-43d4-9b9a-d0613d310768", null, "Admin", "ADMIN" },
                    { "b8c754e8-f0ec-4798-bc2a-a088a98ba3d6", null, "User", "USER" }
                });
        }
    }
}
