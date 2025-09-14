using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarEvents.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "986134cf-47f7-4c76-bdaa-faec8504723f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b67e9aa0-b28b-47ce-b0d3-0b166a026c8f", "AQAAAAIAAYagAAAAEKT265pZfXKM9hkxLxXLyO126BkoqhfqZDGASq2jZ75x6WgIXOULYcoHi75ahOWDcg==", "6bc6cca2-bdac-4421-a760-1e400605cfdc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3643f15-0620-4af9-a729-4fcd4a4fb8bd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46426841-768e-4eae-8f22-524273104da0", "AQAAAAIAAYagAAAAEC/iZ/kDfC16mVYM5xVPjqP1v8QjmINsCCRkAXZEWgLHw0xBsfU4MKqHuOdiqA5T0w==", "143115db-2a02-484f-9869-80f44f005743" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2599235-a01c-4a69-851b-5e569d4b1b35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6e46ffa-cd68-4850-bf28-2a095a763ad8", "AQAAAAIAAYagAAAAECAkfvQ/7Qowcq0bM6AJN0eqV6MAkOTJqZKCo6rRsZgdDuoFnEZfuPyo0qf+61y3Iw==", "64dc90b3-71ca-4357-82c5-c4f8d61deb2d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "986134cf-47f7-4c76-bdaa-faec8504723f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8511fc55-39e6-41ed-9f6c-cbc86d156702", "AQAAAAIAAYagAAAAEKKcz0YLelYkeYrzXI2ngqbwLMLw6PpauuwtBjjLqqSNYOLDGMmXHchu1yvkWqZgWw==", "4bce6868-0563-4042-85ee-b28d5614e404" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3643f15-0620-4af9-a729-4fcd4a4fb8bd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "808db08d-2c03-4775-a78f-d1962f657796", "AQAAAAIAAYagAAAAEHhgstAc8XFFuS2A6UmYg1N6Z+LZfDO8wbXhFxS6+3IPeKUwA60kxK+bS7cLxqr94g==", "3734c165-fa0c-4915-91e6-08631a9fc366" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2599235-a01c-4a69-851b-5e569d4b1b35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5705c9a-759a-4632-9805-0b1b108e39d3", "AQAAAAIAAYagAAAAEKSW0Lsrt5hzQ8DkqV1gK0MSIZ4unwDEJ3qAJeBwAmxCwU4Af7RUARkMYXlaXnJhKg==", "91e942bf-6b7b-4f87-b804-eda53b3a0841" });
        }
    }
}
