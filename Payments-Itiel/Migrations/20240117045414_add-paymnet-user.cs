using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payments_Itiel.Migrations
{
    /// <inheritdoc />
    public partial class addpaymnetuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_UserCreatedId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserCreatedId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserId",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserCreatedId",
                table: "Payment",
                column: "UserCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_UserCreatedId",
                table: "Payment",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
