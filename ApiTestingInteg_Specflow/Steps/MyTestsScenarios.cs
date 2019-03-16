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
        [Given(@"I have an endpoint (.*)")]
        public void GivenIHaveAnEndpointEndpoint(string endpoint)
        {
            RestApiHelper.SetURL(endpoint);
        }

        [When(@"I call the Get method of API")]
        public void WhenICallTheGetMethodOfAPI()
        {
            RestApiHelper.CreateRequest(RestSharp.Method.GET);
        }

        [Then(@"I get API response as statuscode as 200OK")]
        public void ThenIGetAPIResponseAsStatuscodeAsOK()
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            //if (response.StatusCode == HttpStatusCode.OK)
            //{

            //}
        }

        [When(@"I call the method GET to fetch user information using the ID (.*)")]
        public void WhenICallTheMethodGETToFetchUserInformationUsingTheID(string userid)
        {

            RestApiHelper.CreateRequest(userid, RestSharp.Method.GET);
        }

        [Then(@"I will get user information")]
        public void ThenIWillGetUserInformation()
        {
            CustomerDataModel customerJObject;
            string jsonFilePath = @"c:\\customer-data-response.json";

            var response = RestApiHelper.GetResponse();

            var jsonObject = JsonConvert.DeserializeObject<CustomerDataModel>(response.Content);

            customerJObject = RestApiHelper.GetResource<CustomerDataModel>(jsonFilePath);

            Assert.That(jsonObject.id, Is.EqualTo(customerJObject.id));
            Assert.That(jsonObject.title, Is.EqualTo(customerJObject.title));
            Assert.That(jsonObject.author, Is.EqualTo(customerJObject.author));
        }

        [Given(@"I have a endpoint (.*)")]
        public void GivenIHaveAEndpointUserinformation(string endpoint)
        {
            RestApiHelper.SetURL(endpoint);
        }

        [When(@"I call the method GET to fetch user information using the (.*) and (.*)")]
        public void WhenICallTheMethodGETToFetchUserInformationUsingTheAnd(int userId, int accountNumber)
        {
            RestApiHelper.CreateRequest(userId, accountNumber, RestSharp.Method.GET);
        }

        [Then(@"I will get the user information")]
        public void ThenIWillGetTheUserInformation()
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            //if (response.IsSuccessful == true)
            //{

            //}
        }

        [When(@"I call a POST method to register a book")]
        public void WhenICallAPOSTMethodToRegisterABook()
        {
            RestApiHelper.CreatePostRequest();
        }

        [Then(@"I will register the book successfuly")]
        public void ThenIWillRegisterTheBookSuccessfuly()
        {
            var response = RestApiHelper.GetResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            //if (response.IsSuccessful == true)
            //{

            //}
        }

        [When(@"I call a GET method to retrieve a book information with its (.*)")]
        public void WhenICallAGETMethodToRetrieveABookInformationWithIts(string bookId)
        {
            RestApiHelper.CreateGetRequest(bookId, RestSharp.Method.GET);
        }

        [Then(@"I will get the book information for validation")]
        public void ThenIWillGetTheBookInformationForValidation()
        {
            var response = RestApiHelper.GetResponse();

            string schemaFilePath = @"Json\Schema\book-data-schema.json";

            var result = RestApiHelper.ValidateJsonSchema(schemaFilePath, response);

            Assert.That(result, Is.True);

            //if (response.IsSuccessful == true)
            //{

            //}

        }



    }
}
