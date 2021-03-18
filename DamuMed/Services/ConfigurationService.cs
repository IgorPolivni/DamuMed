using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace DamuMed.Services
{
    public static class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {

            DbProviderFactories.RegisterFactory("DamuMedProvider", SqlClientFactory.Instance);

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("Settings.json").Build();
            }
        }
    }
}
