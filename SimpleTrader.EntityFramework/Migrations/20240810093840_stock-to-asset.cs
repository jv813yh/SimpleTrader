using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleTrader.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class stocktoasset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock_Symbol",
                table: "AssetTransactions",
                newName: "Asset_Symbol");

            migrationBuilder.RenameColumn(
                name: "Stock_PricePerShares",
                table: "AssetTransactions",
                newName: "Asset_PricePerShares");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Asset_Symbol",
                table: "AssetTransactions",
                newName: "Stock_Symbol");

            migrationBuilder.RenameColumn(
                name: "Asset_PricePerShares",
                table: "AssetTransactions",
                newName: "Stock_PricePerShares");
        }
    }
}
