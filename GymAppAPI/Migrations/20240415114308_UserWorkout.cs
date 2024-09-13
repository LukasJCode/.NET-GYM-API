using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "448fc2bb-bb62-458f-98e5-8b5062d1fed4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69c487be-36ed-4216-a82a-2214dc8eef59");

            migrationBuilder.CreateTable(
               name: "UserWorkouts",
               columns: table => new
               {
                   AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   WorkoutId = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserWorkouts", x => new { x.AppUserId, x.WorkoutId });
                   table.ForeignKey(
                       name: "FK_UserWorkouts_AspNetUsers_AppUserId",
                       column: x => x.AppUserId,
                       principalTable: "AspNetUsers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserWorkouts_Workouts_WorkoutId",
                       column: x => x.WorkoutId,
                       principalTable: "Workouts",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "112317f7-2d92-42f7-9072-37ef2682d689", null, "Admin", "ADMIN" },
                    { "90715769-43e8-4c86-aa6d-34d9f6f86e08", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "112317f7-2d92-42f7-9072-37ef2682d689");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90715769-43e8-4c86-aa6d-34d9f6f86e08");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserWorkouts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "448fc2bb-bb62-458f-98e5-8b5062d1fed4", null, "User", "USER" },
                    { "69c487be-36ed-4216-a82a-2214dc8eef59", null, "Admin", "ADMIN" }
                });
        }
    }
}
