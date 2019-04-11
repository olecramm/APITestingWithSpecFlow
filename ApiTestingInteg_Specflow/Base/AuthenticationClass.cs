using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;

namespace ApiTestingInteg_Specflow.Base
{
    public interface IAuthenticationClass
    {
        Task<string> GetOauthAuthenticationAsync();
    }

    public class AuthenticationClass : IAuthenticationClass
    {
        private string BaseUrl { get; set; }
        private string ResourcePath { get; set; }
        private JObject ParamsObj { get; set; }

        IRestClient authClient;
        IRestRequest authRequest;

        public AuthenticationClass(string baseUrl, JObject paramsObj)
        {
            BaseUrl = baseUrl;
            ParamsObj = paramsObj;
        }

        public async Task<string> GetOauthAuthenticationAsync()
        {
            authClient = new RestClient(BaseUrl);
            authRequest = new RestRequest(ResourcePath, Method.POST);

            authRequest.RequestFormat = DataFormat.Json;
            authRequest.AddJsonBody(ParamsObj);

            var response = await authClient.ExecuteTaskAsync(authRequest);

            var accessToken = JObject.Parse(response.Content);

            var token = accessToken.GetValue("access_token").ToString();

            return token;
        }
    }
}
