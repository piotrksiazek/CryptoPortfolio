using Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager)
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();

            try
            {
                if (!context.Cryptocurrencies.Any())
                {
                    //Cryptocurrency seed
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

                //User seed
                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        UserName = "TestUser",
                        Email = "TestUser@mail.com",
                        Wallets = new List<Wallet>
                            {
                                new()
                                {
                                    Address = "0x94ceb946E97B928B71B3dD29046585Efd4cdF7f7", //some random address
                                    AppUserId = "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    WalletTypeId = 0,
                                    WalletType = new WalletType()
                                    {
                                        Name = "Ethereum"
                                    }
                                }
                            },
                        Notifications = new List<Notification>()
                            {
                                new()
                                {
                                    PricePoint = 1500,
                                    IsRecurring = true,
                                    AppUserId = "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    CryptocurrencyId = 5
                                }
                            },
                        Balances = new List<Balance>()
                            {
                                new()
                                {
                                    Amount = 420,
                                    AppUserId = "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    CryptocurrencyId = 5
                                }
                            },
                        Transactions = new List<Transaction>()
                            {
                                new()
                                {
                                    UnitPrice = 3200M,
                                    Amount = 12M,
                                    AppUserId= "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    CryptocurrencyId= 5
                                },
                                new()
                                {
                                    UnitPrice = 3242M,
                                    Amount = 1.5M,
                                    AppUserId= "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    CryptocurrencyId= 5
                                },
                                new()
                                {
                                    UnitPrice = 3801M,
                                    Amount = 12.5M,
                                    AppUserId= "697f3bf6-439e-46a6-af4b-95dcb5a79b1a",
                                    CryptocurrencyId= 5
                                }
                            }
                    };

                    await userManager.CreateAsync(user, "P@$$w0rd");
                }
            }

            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
