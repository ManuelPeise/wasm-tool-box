using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ExpieresAt", "Password", "Salt", "Token", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5994), "System", new DateTime(2024, 3, 11, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5994), "UEBzc3dvcmRlYmIxN2RlZi1hZjY1LTQxOTItOTczNC1jN2NhMjI1MTk4NDc=", "ebb17def-af65-4192-9734-c7ca22519847", "", new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5994), "System" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5626), "System", "Role: Admin", "Admin", new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5626), "System" },
                    { 2, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5626), "System", "Role: User", "User", new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(5626), "System" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CredentialsId", "Email", "FailedLogins", "FirstName", "IsActive", "LastName", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(6797), "System", 1, "admin@devtools.com", 0, "", true, "", new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(6797), "System" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AppuserId", "CreatedAt", "CreatedBy", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(7144), "System", 1, new DateTime(2023, 12, 12, 13, 14, 11, 425, DateTimeKind.Utc).AddTicks(7144), "System" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
