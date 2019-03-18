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
        private static string baseURL = "http://localhost:3000";
        private static string rootProject = @"C:\Users\MNZZ\source\repos\APITesting_Spacflow_Framework\ApiTestingInteg_Specflow\";

        public static RestClient SetURL(string endpoint)
        {
            var url = baseURL+endpoint;
            return client = new RestClient(url);
        }

        public static RestRequest CreateRequest(Method method)
        {
            restRequest = new RestRequest(method);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static IRestRequest CreateRequest(string userId, Method method)
        {
            restRequest = new RestRequest(userId,method);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static IRestRequest CreateRequest(int userId, int accountNumber, Method method)
        {
            var resourcePath = $"?userId={userId}&account={accountNumber}";
            restRequest = new RestRequest(resourcePath, method);
            restRequest.AddHeader("Accept", "Application/json");
            return restRequest;
        }

        public static IRestRequest CreatePostRequest()
        {
            var jsonFilePath = @"Json\Request\book-data-request.json";
            var objData = GetResource<BookDataModel>(jsonFilePath);

            restRequest = new RestRequest(Method.POST);
            restRequest.AddJsonBody(objData);
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

        public static IRestRequest CreateGetRequest(string bookId, Method method)
        {
            restRequest = new RestRequest(bookId, method);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static string GetResource(string resourceFilePath)
        {
            var fullPath = Path.Combine(rootProject, resourceFilePath);
            var dataString = string.Empty;

            using (StreamReader file = File.OpenText(fullPath))
            {
                dataString = file.ReadToEnd();
            }

            return dataString;
        }

        public static bool ValidateJsonSchema(string filePath, IRestResponse response)
        {
            var schemaString = RestApiHelper.GetResource(filePath);

            JsonSchema schema = JsonSchema.Parse(schemaString);

            JObject jsonObject = JObject.Parse(response.Content);

            return jsonObject.IsValid(schema);
        }
    }
}
