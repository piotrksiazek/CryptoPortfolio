using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
            try
            {
                logger.LogError("InsideSeedAsync");
                if (!context.Cryptocurrencies.Any())
                {
                    
                    var bitcoin = new Cryptocurrency()
                    {
                        Name = "Bitcoin",
                        Symbol = "BTC",
                        CoingeckoName = "bitcoin"
                    };

                    var ethereum = new Cryptocurrency()
                    {
                        Name = "Ethereum",
                        Symbol = "ETH",
                        CoingeckoName = "ethereum"
                    };

                    var usdt = new Cryptocurrency()
                    {
                        Name = "Tether",
                        Symbol = "USDT",
                        CoingeckoName = "usdt"
                    };

                    context.Cryptocurrencies.AddRange(bitcoin, ethereum, usdt);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
