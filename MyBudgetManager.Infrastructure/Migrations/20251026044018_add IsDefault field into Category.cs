using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudgetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIsDefaultfieldintoCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Categories");
        }
    }
}
