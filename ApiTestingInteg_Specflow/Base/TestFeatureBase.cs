using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;

namespace ApiTestingInteg_Specflow.Base
{
    public interface ITestFeatureBase
    {
        IRestClient SetURL(string Url);
        IRestRequest SetResourcePath(string resourcePath);
        string SetResourceParams(string resourceParams);
        IRestRequest SetHttpMethod(Method method);
        IRestRequest SetHeaderToken(string token);
        IRestRequest AddJsonBody(JObject jsonBodyObj);
        T GetResource<T>(string filePath);
        string GetResource(string filePath);
        IRestResponse GetResponse();
        Task<IRestResponse> GetResponseAsync();
        bool ValidateJsonSchema(string filePath, IRestResponse response);
    }

    public class TestFeatureBase : ITestFeatureBase
    {
        private string BaseURL { get; set; }
        private string ResourcePath { get; set; }
        private string ResourceParams { get; set; }
        private string Token { get; set; }
        private JObject JsonBodyObj { get; set; }

        private IRestClient restClient;
        private IRestRequest restRequest;
        public JObject _config;

        public TestFeatureBase()
        {
            //restClient = new RestClient();
            //restRequest = new RestRequest();

            _config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appSettings.json")
                .Build();
        }

        public IRestClient SetURL(string Url)
        {
            restClient = new RestClient(Url);
         
            return restClient;
        }

        public IRestRequest SetResourcePath(string resourcePath)
        {
            restRequest = new RestRequest(resourcePath);

            return restRequest;            
        }

        public IRestRequest SetHttpMethod(Method method)
        {
            restRequest.Method = method;
            restRequest.RequestFormat = DataFormat.Json;

            return restRequest;
        }

        public IRestRequest SetHeaderToken(string token)
        {
            restRequest.AddHeader("Authentication", string.Format("Bearer {0}", token));
            return restRequest;
        }

        public IRestRequest AddJsonBody(JObject jsonBodyObj)
        {
            restRequest.AddJsonBody(jsonBodyObj);
            return restRequest;
        }

        public T GetResource<T>(string filePath)
        {
            T jsonObjectData = default(T);
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonObjectData = (T)serializer.Deserialize(file, typeof(T));
            }
            return jsonObjectData;
        }

        public string GetResource(string filePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            var dataString = string.Empty;

            using (StreamReader file = File.OpenText(fullPath))
            {
                dataString = file.ReadToEnd();
            }

            return dataString;
        }

        public IRestResponse GetResponse()
        {
            return restClient.Execute(restRequest);
        }

        public Task<IRestResponse> GetResponseAsync()
        {
            return restClient.ExecuteTaskAsync(restRequest);
        }

        public string SetResourceParams(string resourceParams)
        {
            ResourceParams = ResourcePath+resourceParams;
            restRequest.RequestFormat = DataFormat.Json;

            return ResourceParams;
        }

        public bool ValidateJsonSchema(string filePath, IRestResponse response)
        {
            var schemaString = GetResource(filePath);

            JsonSchema schema = JsonSchema.Parse(schemaString);

            JObject jsonObject = JObject.Parse(response.Content);

            return jsonObject.IsValid(schema);
        }
    }
}
