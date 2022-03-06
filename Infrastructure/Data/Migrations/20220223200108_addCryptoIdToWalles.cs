using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class addCryptoIdToWalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CryptocurrencyId",
                table: "Wallets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CryptocurrencyId",
                table: "Wallets",
                column: "CryptocurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Cryptocurrencies_CryptocurrencyId",
                table: "Wallets",
                column: "CryptocurrencyId",
                principalTable: "Cryptocurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Cryptocurrencies_CryptocurrencyId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CryptocurrencyId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CryptocurrencyId",
                table: "Wallets");
        }
    }
}
