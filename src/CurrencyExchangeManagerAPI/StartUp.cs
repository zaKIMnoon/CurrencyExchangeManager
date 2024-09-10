using CurrencyExchangeManagerAPI.Services;
using CurrencyExchangeManagerLib.Connections;
using CurrencyExchangeManagerLib.Repositories;
using CurrencyExchangeManagerLib.Settings;
using CurrencyExchangeManagerLib.SystemImports;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;

namespace CurrencyExchangeManagerAPI
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public StartUp(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(DatabaseConfig.NewDatabaseConfig(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(ApiConfig.NewAPIConfig(Configuration.GetConnectionString("APIKey")));
            services.AddSingleton<SourceSystemRepository>();
            services.AddSingleton<CurrencyRepository>();
            services.AddSingleton<CurrencyRateRepository>();
            services.Configure<BackgroundServiceSettings>(Configuration.GetSection("BackgroundServiceSettings"));
            services.AddHostedService<CurrencyService>();
        }
    }
}
