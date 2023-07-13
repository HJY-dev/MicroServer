using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServer.Common.Configuration
{
    public class ConfigurationManager
    {
        public static IConfiguration GetConfiguration(string jsonFile)
        {
            var builder = new ConfigurationBuilder()
                        .AddJsonFile(jsonFile, optional: true, reloadOnChange: true);


            IConfiguration config = builder.Build();
            return config;
        }

        private static IConfiguration appsettings;
        public static IConfiguration Appsettings
        {
            get
            {
                if (appsettings == null)
                    appsettings = GetConfiguration("appsettings.json");
                return appsettings;
            }
            set
            {
                if (appsettings != value)
                    appsettings = value;
            }
        }
    }
}
