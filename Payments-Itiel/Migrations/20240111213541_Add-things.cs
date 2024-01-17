using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payments_Itiel.Migrations
{
    /// <inheritdoc />
    public partial class Addthings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payment");
        }
    }
}
