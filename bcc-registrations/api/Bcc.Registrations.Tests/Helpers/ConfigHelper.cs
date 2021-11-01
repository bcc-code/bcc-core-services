using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcc.Registrations.Tests.Helpers
{
    public class ConfigHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("8cc53bcb-55db-403c-a24b-ac3cb079ac1c") //Should match user secrets ID in project file
                .AddEnvironmentVariables()
                .Build();
        }

        public static T GetConfig<T>(string outputPath, string section)
        {
            var configuration = Activator.CreateInstance<T>();

            var configRoot = GetIConfigurationRoot(outputPath);

            configRoot
                .GetSection(section)
                .Bind(configuration);

            return configuration;
        }
    }
}
