using ApiTestingInteg_Specflow.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.IO;

namespace ApiTestingInteg_Specflow.Base
{
    public class RestApiHelper
    {
        private static RestClient client;
        private static RestRequest restRequest;

        public static RestClient SetURL(string url)
        {
            return client = new RestClient(url);
        }

        public static IRestRequest CreateGetRequest(string resourcePath)
        {
            if (resourcePath == null)
            {
                restRequest = new RestRequest(Method.GET);
            }
            else
            {
                restRequest = new RestRequest(resourcePath, Method.GET);
            }

            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static IRestRequest CreatePostRequest(string jsonRequestFilaPath)
        {            
            var objRequestData = GetResource<BookDataModel>(jsonRequestFilaPath);

            restRequest = new RestRequest(Method.POST);
            restRequest.AddJsonBody(objRequestData);
            restRequest.AddHeader("Accept", "application/json");

            return restRequest;
        }

        public static T GetResource<T>(string resourcePath)
        {
            T jsonObjectData = default(T);
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcePath);

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonObjectData = (T)serializer.Deserialize(file, typeof(T));
            }
            return jsonObjectData;
        }

        public static IRestResponse GetResponse()
        {
            return client.Execute(restRequest);
        }

        public static string GetResource(string resourceFilePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourceFilePath);
            var dataString = string.Empty;

            using (StreamReader file = File.OpenText(fullPath))
            {
                dataString = file.ReadToEnd();
            }

            return dataString;
        }

        public static bool ValidateJsonSchema(string filePath, IRestResponse response)
        {
            var schemaString = GetResource(filePath);

            JsonSchema schema = JsonSchema.Parse(schemaString);

            JObject jsonObject = JObject.Parse(response.Content);

            return jsonObject.IsValid(schema);
        }
    }
}
