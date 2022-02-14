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
                if (!context.Cryptocurrencies.Any())
                {
                    List<Cryptocurrency> cryptocurrencies = new()
                    {
                        new()
                        {
                            Name = "Bitcoin",
                            Symbol = "BTC",
                            CoingeckoName = "bitcoin"
                        },
                        new()
                        {
                            Name = "Ethereum",
                            Symbol = "ETH",
                            CoingeckoName = "ethereum"
                        },
                        new()
                        {
                            Name = "Tether",
                            Symbol = "USDT",
                            CoingeckoName = "usdt"
                        }
                    };
                    context.Cryptocurrencies.AddRange(cryptocurrencies);
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
