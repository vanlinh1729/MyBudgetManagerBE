using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudgetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfieldintoTokenstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TokenValue",
                table: "Tokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeviceInfo",
                table: "Tokens",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Tokens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplacedByToken",
                table: "Tokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TokenValue",
                table: "Tokens",
                column: "TokenValue",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_TokenValue",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "DeviceInfo",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ReplacedByToken",
                table: "Tokens");

            migrationBuilder.AlterColumn<string>(
                name: "TokenValue",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
