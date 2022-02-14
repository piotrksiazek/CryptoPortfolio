using Core.Interfaces;
using Infrastructure.Data.Repositories;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ICryptocurrencyRepository, CryptocurrencyRepository>();
            return services;
        }
    }
}
