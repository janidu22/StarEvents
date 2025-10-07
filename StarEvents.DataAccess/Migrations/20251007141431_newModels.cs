using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarEvents.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "986134cf-47f7-4c76-bdaa-faec8504723f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "980a2a58-cabe-4ac3-9dd1-f8dc605f34b9", "AQAAAAIAAYagAAAAEEWT3n5lgujVR0m2Po5EvFztzxTsy/vYpJq5rITOKoduAg4ZX4Qfa9idarheN1qg9A==", "bb40e9b3-67c6-44c7-b6c9-0f2db53cdc26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3643f15-0620-4af9-a729-4fcd4a4fb8bd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b69bcfdc-67a2-45cc-9574-165b39669296", "AQAAAAIAAYagAAAAEAPsnr7LNFYSRY7PcEyernbS0JSL4sX60jneCm+obVc/Wp9hY9iIGiDO55V5EmKjqQ==", "5dd4a459-7b7f-4ada-991f-30266a75e857" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2599235-a01c-4a69-851b-5e569d4b1b35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "122d7722-617d-440b-ba53-6d7b2b7eb913", "AQAAAAIAAYagAAAAEDzajS/TTWplZXD3+EaS7EQ8BATAsCDILCOm4Ml64mZVa2DpUxxK5ANlOu86Nyo4ow==", "9d6672a7-cddf-408b-861a-791e19e3afe7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
