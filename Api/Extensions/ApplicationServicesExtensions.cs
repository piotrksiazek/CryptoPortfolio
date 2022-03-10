using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Auth;
using Infrastructure;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;

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
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsRetriever, ClaimsRetriever>();
            return services;
        }
    }
}
