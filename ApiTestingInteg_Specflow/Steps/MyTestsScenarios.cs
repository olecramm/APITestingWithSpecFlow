using ApiTestingInteg_Specflow.Base;
using ApiTestingInteg_Specflow.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;

namespace ApiTestingInteg_Specflow.Steps
{
    [Binding]
    public sealed class MyTestsScenarios
    {
        [Given(@"I have a BaseHost (.*)")]
        public void GivenIHaveABaseHost(string baseHost)
        {
            ScenarioContext.Current.Add("BaseHost", baseHost);
        }

        [Given(@"I have an endpoint (.*)")]
        public void GivenIHaveAnEndpointEndpoint(string endpoint)
        {
            var baseHost = string.Empty;
            ScenarioContext.Current.TryGetValue("BaseHost", out baseHost);

            RestApiHelper.SetURL(baseHost+endpoint);
        }

        [When(@"I call the Get method of API")]
        public void WhenICallTheGetMethodOfAPI()
        {
            string resourcePath = null;

            RestApiHelper.CreateGetRequest(resourcePath);
        }

        [Then(@"I get API response as statuscode as (.*)")]
        public void ThenIGetAPIResponseAsStatuscodeAs(string statusCode)
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(statusCode));
        }

        [When(@"I call the method GET to fetch user information using the ID (.*)")]
        public void WhenICallTheMethodGETToFetchUserInformationUsingTheID(string userid)
        {

            RestApiHelper.CreateGetRequest(userid);
        }

        [Then(@"I will get user information as like as expected file @""(.*)""")]
        public void ThenIWillGetUserInformationAsLikeAsExpectedFile(string fileExpectedResultPath)
        {
            var teste = fileExpectedResultPath;

            CustomerDataModel custJObjectExpected;

            var response = RestApiHelper.GetResponse();

            var jsonObject = JsonConvert.DeserializeObject<CustomerDataModel>(response.Content);

            custJObjectExpected = RestApiHelper.GetResource<CustomerDataModel>(teste);

            Assert.That(jsonObject.id, Is.EqualTo(custJObjectExpected.id));
            Assert.That(jsonObject.title, Is.EqualTo(custJObjectExpected.title));
            Assert.That(jsonObject.author, Is.EqualTo(custJObjectExpected.author));
        }

        [Given(@"I have a endpoint (.*)")]
        public void GivenIHaveAEndpointUserinformation(string endpoint)
        {
            RestApiHelper.SetURL(endpoint);
        }

        [When(@"I call the method GET to fetch user information using the (.*) and (.*)")]
        public void WhenICallTheMethodGETToFetchUserInformationUsingTheAnd(int userId, int accountNumber)
        {
            var resourcePath = $"?userId={userId}&account={accountNumber}";

            RestApiHelper.CreateGetRequest(resourcePath);
        }

        [Then(@"I will get the response statuscode as (.*)")]
        public void ThenIWillGetTheResponseStatuscodeAs(string expectedResult)
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(expectedResult));
        }


        [Then(@"I will get the user information")]
        public void ThenIWillGetTheUserInformation()
        {
            
        }

        [When(@"I call a POST method with the request @""(.*)"" to register a book")]
        public void WhenICallAPOSTMethodWithTheRequestToRegisterABook(string jsonRequestFilePath)
        {
            RestApiHelper.CreatePostRequest(jsonRequestFilePath);
        }

        [Then(@"I will register the book successfuly returning statuscode as (.*)")]
        public void ThenIWillRegisterTheBookSuccessfulyReturningStatuscodeAs(string expectedResult)
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode.ToString(), Is.EqualTo(expectedResult));
        }

        [When(@"I call a GET method to retrieve a book information with its (.*)")]
        public void WhenICallAGETMethodToRetrieveABookInformationWithIts(string bookId)
        {
            string resourcePath = bookId;
            RestApiHelper.CreateGetRequest(resourcePath);
        }

        [Then(@"I will get the book information as json to validate jsonschema as @""(.*)""")]
        public void ThenIWillGetTheBookInformationAsJsonToValidateJsonschemaAs(string expectedResultPath)
        {
            var response = RestApiHelper.GetResponse();

            var result = RestApiHelper.ValidateJsonSchema(expectedResultPath, response);

            Assert.That(result, Is.True);
        }
    }
}
