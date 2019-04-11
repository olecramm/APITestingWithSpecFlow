using ApiTestingInteg_Specflow.Base;
using ApiTestingInteg_Specflow.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;

namespace ApiTestingInteg_Specflow.Steps
{
    [Binding]
    public sealed class MyTestsScenarios : TestFeatureBase
    {
        private string token;

        [Given(@"I have a BaseHost '(.*)'")]
        public void GivenIHaveABaseHost(string baseHost)
        {
            ScenarioContext.Current.Add("BaseHost", baseHost);
            SetURL(baseHost);
        }

        [Given(@"I have a oauth BaseHost '(.*)'")]
        public void GivenIHaveAOauthBaseHost(string authBaseHost)
        {
            ScenarioContext.Current.Add("authBaseHost", authBaseHost);
        }

        [Given(@"I have endpoint (.*) the params (.*)")]
        public void GivenIHaveEndpointPostsTheParams(string endpoint, string resourcePath)
        {
            SetResourcePath(endpoint + resourcePath);
        }


        [When(@"I call the Get method of API")]
        public void WhenICallTheGetMethodOfAPI()
        {
            SetHttpMethod(Method.GET);
        }

        [Then(@"I get API response as statuscode as (.*)")]
        public void ThenIGetAPIResponseAsStatuscodeAs(string statusCode)
        {
            var response = GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(statusCode));
        }

        //[Given(@"I have an endpoint (.*) and fetch user information using the ID (.*)")]
        //public void GivenIHaveAnEndpointPostsAndFetchUserInformationUsingTheID(string endpoint, string resourcePath)
        //{
        //    SetResourcePath(endpoint + resourcePath);
        //}


        [When(@"I call the method GET of API")]
        public void WhenICallTheMethodGETOfAPI()
        {
            SetHttpMethod(Method.GET);
        }

        [Then(@"I will get user information as like as expected file @""(.*)""")]
        public void ThenIWillGetUserInformationAsLikeAsExpectedFile(string fileExpectedResultPath)
        {
            CustomerDataModel custJObjectExpected;

            var response = GetResponse();

            var jsonObject = JsonConvert.DeserializeObject<CustomerDataModel>(response.Content);

            custJObjectExpected = GetResource<CustomerDataModel>(fileExpectedResultPath);

            Assert.That(jsonObject.id, Is.EqualTo(custJObjectExpected.id));
            Assert.That(jsonObject.title, Is.EqualTo(custJObjectExpected.title));
            Assert.That(jsonObject.author, Is.EqualTo(custJObjectExpected.author));
        }

        [Given(@"I have an endpoint (.*) and using the params (.*) and (.*)")]
        public void GivenIHaveAnEndpointUserinformationAndUsingTheParamsAnd(string endpoint, int userId, int accountNumber)
        {
            var resourceParams = $"?userId={userId}&account={accountNumber}";

            SetResourcePath(endpoint + resourceParams);
        }

        [When(@"I call the method GET to fetch user information")]
        public void WhenICallTheMethodGETToFetchUserInformationUsingTheAnd()
        {
            SetHttpMethod(Method.GET);
        }

        [Then(@"I will get the response statuscode as (.*)")]
        public void ThenIWillGetTheResponseStatuscodeAsOK(string expectedResult)
        {
            var response = GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(expectedResult));
        }

        //[Given(@"I have an endpoint (.*)")]
        //public void GivenIHaveAnEndpointPosts(string endpoint)
        //{
        //    SetResourcePath(endpoint);
        //}


        [When(@"I call a POST method with the request @""(.*)"" to register a book")]
        public void WhenICallAPOSTMethodWithTheRequestToRegisterABook(string jsonRequestFilePath)
        {
            SetHttpMethod(Method.POST);
            var jsonObj = JObject.Parse(GetResource(jsonRequestFilePath));
            AddJsonBody(jsonObj);
        }

        [Then(@"I will register the book successfuly returning statuscode as (.*)")]
        public void ThenIWillRegisterTheBookSuccessfulyReturningStatuscodeAs(string expectedResult)
        {
            var response = GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(expectedResult));
        }

        [Given(@"I have endpoint (.*) with its (.*)")]
        public void GivenIHaveEndpointPostsWithIts(string endpoint, string resourcePath)
        {
            SetResourcePath(endpoint + resourcePath);
        }

        [When(@"I call a GET method to retrieve a book information")]
        public void WhenICallAGETMethodToRetrieveABookInformation()
        {
            SetHttpMethod(Method.GET);
        }


        [Then(@"I will get the book information as json to validate jsonschema as @""(.*)""")]
        public void ThenIWillGetTheBookInformationAsJsonToValidateJsonschemaAs(string expectedResultPath)
        {
            var response = GetResponse();

            var result = ValidateJsonSchema(expectedResultPath, response);

            Assert.That(result, Is.True);
        }

        [When(@"I call a Get Method with the token embedded in the request")]
        public async void WhenICallAGetMethodWithTheTokenEmbeddedInTheRequest()
        {
            var oauthBaseHost = _config.SelectToken("endpoint")["authEndpoint"].ToString();
            var requestParamsObj = JObject.Parse(_config.SelectToken("authBodyParams").ToString());
            IAuthenticationClass restApiAuthentication = new AuthenticationClass(oauthBaseHost, requestParamsObj);
            token = await restApiAuthentication.GetOauthAuthenticationAsync();

            SetHttpMethod(Method.GET);

            SetHeaderToken(token);
        }

        [Given(@"I have endpoint (.*) passing (.*)")]
        public void GivenIHaveEndpointUserinformationPassing(string endpoint, string resourcePath)
        {
            SetResourcePath(endpoint + resourcePath);
        }

        [Then(@"System resturns user information as @""(.*)""")]
        public void ThenSystemResturnsUserInformationAs(string expectedResult)
        {
            var response = GetResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
