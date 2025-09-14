using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarEvents.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class loyaliryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LoyaltyPointsHistories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LoyaltyPointsHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LoyaltyPointsHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LoyaltyPointsHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "986134cf-47f7-4c76-bdaa-faec8504723f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82215b43-1209-4e85-87f1-a0dc7d0e49f7", "AQAAAAIAAYagAAAAEEOxtPpTujs7lHoxV5jYL7N22C/Jc+N+BRezrqmbMC+xLPF1G0ePJSkTH3ljAnMDYQ==", "6f05b18f-7a8e-4d22-b1ae-b1e81d18d95f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3643f15-0620-4af9-a729-4fcd4a4fb8bd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba3a9dc0-d57c-48e6-8b32-f296adcd330a", "AQAAAAIAAYagAAAAEAaF3WT4vxO8Z5uknXS0juBIBumxfG+IW9VRSCWqwiU3gRLyv3gk5b9J4fGOn+If5g==", "5fa85f9a-1838-49ec-b658-36602e7eacdd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2599235-a01c-4a69-851b-5e569d4b1b35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a04aafef-813d-4834-8f85-6e0c42f7bb79", "AQAAAAIAAYagAAAAEF60EL1iyHzv9YgtVElMmornFGpfH68qJxXvSkTgQbiTlsgZGFXYTciGkvQUjiArHQ==", "39dc2cb7-9946-4929-98b4-ed81a85a4468" });
        }
    }
}
