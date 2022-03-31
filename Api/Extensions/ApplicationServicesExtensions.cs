using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Auth;
using Infrastructure;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.BackgroundServices;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IUserOwnedRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsRetriever, ClaimsRetriever>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IMailingService, MailingService>();
            services.AddSingleton<IExternalApiClientService, ExternalApiClientService>();
            services.AddSingleton<ICryptoApiCallerService, CryptoApiCallerService>();
            services.AddSingleton<IPriceChangeNotificationService, PriceChangeNotificationService>();

            services.AddHostedService<PriceChangeNotificationServiceRunner>();

            return services;
        }
    }
}
