using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnedayOneDev_Shared.Utils
{
    public class AppSettings
    {
         public string apiUrl { get; private set; }
         public string appName { get; private set; }

        private string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ,"AppSettings.json");
        public AppSettings() 
        {
            
            if(!File.Exists(ConfigFilePath))
            {
                throw new FileNotFoundException("Fichier de configuration introuvable", ConfigFilePath);
            }
        
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
            .Build();

            apiUrl = configuration["Api:BaseUrl"];
            appName = configuration["App:Name"];
        }

    }
}
