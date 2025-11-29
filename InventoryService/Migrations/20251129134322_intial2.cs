using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class intial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitlements_Users_UserId1",
                table: "Entitlements");

            migrationBuilder.DropIndex(
                name: "IX_Entitlements_UserId1",
                table: "Entitlements");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Entitlements");

            migrationBuilder.AddColumn<int>(
                name: "Assigned",
                table: "Licenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Entitlements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entitlements_UserId",
                table: "Entitlements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entitlements_Users_UserId",
                table: "Entitlements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitlements_Users_UserId",
                table: "Entitlements");

            migrationBuilder.DropIndex(
                name: "IX_Entitlements_UserId",
                table: "Entitlements");

            migrationBuilder.DropColumn(
                name: "Assigned",
                table: "Licenses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Entitlements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Entitlements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entitlements_UserId1",
                table: "Entitlements",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Entitlements_Users_UserId1",
                table: "Entitlements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
