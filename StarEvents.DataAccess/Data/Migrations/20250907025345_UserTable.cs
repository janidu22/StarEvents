using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarEventsWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LoyaltyPoints",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1", null, "Event Organizer", "EVENT ORGANIZER" },
                    { "41828726-e6de-49c8-a669-2f77fb515474", null, "Admin", "ADMIN" },
                    { "bf0c2771-2974-40ef-8309-0ceb0d2bc93b", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "LoyaltyPoints", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "986134cf-47f7-4c76-bdaa-faec8504723f", 0, "f484b8bb-4bcd-48b6-908f-946623870560", "orgarnizer@test.com", true, "Star Events Organizer", false, null, 1000, "ORGARNIZER@TEST.COM", "ORGARNIZER", "AQAAAAIAAYagAAAAECYVUAOmwjDnNtX9rzMJLqBIQGxTtZHOK8SVpwytlOBh3eac1cBuehjp86cTUq6/hQ==", null, false, "6850166d-1c90-4b76-bc97-e7c32503d8e8", false, "Orgarnizer" },
                    { "c3643f15-0620-4af9-a729-4fcd4a4fb8bd", 0, "01be4711-e6f3-4e08-8459-126e64932fe4", "janidu@test.com", true, "Janidu Dhakshitha Yapa", false, null, 1000, "JANIDU@TEST.COM", "JANIDU", "AQAAAAIAAYagAAAAEM08FEHRTSBR8uRlcJvgQ4ctNYQunVC6nUs/l/z6RV1CR4qe3zbpg8JcTXfU3Cv3xQ==", null, false, "fb3ccac3-abae-48a6-a0a4-793153913db7", false, "Janidu" },
                    { "e2599235-a01c-4a69-851b-5e569d4b1b35", 0, "a1f13c0f-83de-4b4b-a053-ece3fb1fc34e", "admin@test.com", true, "Star Events Admin", false, null, 1000, "ADMIN@TEST.COM", "ADMIN", "AQAAAAIAAYagAAAAEIxsdPRIANX0wyZw1gIRWoXoq9V5bGtOR9B7Iy7FBohdR+hIVxxYFXwhnz6q/vxg3w==", null, false, "0b53db01-054f-46b1-8bab-a3346dd994ba", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1", "986134cf-47f7-4c76-bdaa-faec8504723f" },
                    { "bf0c2771-2974-40ef-8309-0ceb0d2bc93b", "c3643f15-0620-4af9-a729-4fcd4a4fb8bd" },
                    { "41828726-e6de-49c8-a669-2f77fb515474", "e2599235-a01c-4a69-851b-5e569d4b1b35" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1", "986134cf-47f7-4c76-bdaa-faec8504723f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bf0c2771-2974-40ef-8309-0ceb0d2bc93b", "c3643f15-0620-4af9-a729-4fcd4a4fb8bd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "41828726-e6de-49c8-a669-2f77fb515474", "e2599235-a01c-4a69-851b-5e569d4b1b35" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2be05559-cf59-4ae1-9ec0-2f2a8447d2d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41828726-e6de-49c8-a669-2f77fb515474");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf0c2771-2974-40ef-8309-0ceb0d2bc93b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "986134cf-47f7-4c76-bdaa-faec8504723f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3643f15-0620-4af9-a729-4fcd4a4fb8bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2599235-a01c-4a69-851b-5e569d4b1b35");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "AspNetUsers");
        }
    }
}
