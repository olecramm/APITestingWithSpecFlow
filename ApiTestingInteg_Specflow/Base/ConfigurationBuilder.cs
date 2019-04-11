using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ApiTestingInteg_Specflow.Base
{
    public interface IConfigurationBuilder{

        IConfigurationBuilder SetBasePath(string environment);
        IConfigurationBuilder AddJsonFile(string jsonFile);
        JObject Build();
    }

    public class ConfigurationBuilder:IConfigurationBuilder
    {
        private string BasePath { get; set; }
        private string JsonFile { get; set; }
        private JObject Object { get; set; }

        public IConfigurationBuilder AddJsonFile(string jsonFile)
        {
            JsonFile = jsonFile;
            return this;
        }

        public JObject Build()
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, JsonFile);
            var dataString = string.Empty;

            using (StreamReader file = File.OpenText(fullPath))
            {
                dataString = file.ReadToEnd();
            }

            Object = JObject.Parse(dataString);

            return Object;
        }

        public IConfigurationBuilder SetBasePath(string environment)
        {
            BasePath = environment;
            return this;
        }


    }
}
