using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SplunkTagManager.Models;
namespace SplunkTagManager
{
    public class CosmonautConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosConfig = new AppSettingsSection();
            configuration.GetSection("CosmosDb").Bind(cosmosConfig);
            
            var connectionPolicy = new ConnectionPolicy()
            {
                ConnectionProtocol = Protocol.Tcp,
                ConnectionMode = ConnectionMode.Direct
            };
            
            var cosmosSettings = new CosmosStoreSettings(
                cosmosConfig.CosmosDatabaseName,
                cosmosConfig.CosmosDatabaseUrl,
                cosmosConfig.CosmosAuthKey,
                connectionPolicy,
                null,
                cosmosConfig.DefaultConnectionThroughput,
                false,
                cosmosConfig.MaximumUpscaleRequestUnits);

            AddCosmosStores(services, cosmosSettings);

        }

        private static void AddCosmosStores(IServiceCollection services, CosmosStoreSettings cosmosSettings)
        {
            services.AddCosmosStore<Index>(cosmosSettings);
            services.AddCosmosStore<Location>(cosmosSettings);
            services.AddCosmosStore<Tag>(cosmosSettings);
        }


        public class AppSettingsSection
        {
            public AppSettingsSection()
            {
                DefaultConnectionThroughput = 400;
                MaximumUpscaleRequestUnits = 1200;
            }
            
            public string CosmosDatabaseName { get; set; }
            public string CosmosDatabaseUrl { get; set; }
            public string CosmosAuthKey { get; set; }
            public int DefaultConnectionThroughput { get; set; }
            public int MaximumUpscaleRequestUnits { get; set; }
        }
    }
}