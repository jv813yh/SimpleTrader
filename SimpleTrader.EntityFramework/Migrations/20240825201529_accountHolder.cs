using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleTrader.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class accountHolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Accounts",
                newName: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountHolderId",
                table: "Accounts",
                column: "AccountHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_AccountHolderId",
                table: "Accounts",
                column: "AccountHolderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_AccountHolderId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountHolderId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountHolderId",
                table: "Accounts",
                newName: "idUser");
        }
    }
}
