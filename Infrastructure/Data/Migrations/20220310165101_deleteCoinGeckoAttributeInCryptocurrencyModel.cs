using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class deleteCoinGeckoAttributeInCryptocurrencyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoingeckoName",
                table: "Cryptocurrencies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoingeckoName",
                table: "Cryptocurrencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
