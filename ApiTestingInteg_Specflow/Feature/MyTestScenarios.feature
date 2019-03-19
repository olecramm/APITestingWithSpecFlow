Feature: MyTestsScenarios

Background: 
Given I have a BaseHost http://localhost:3000

@TestIntegs
#Testing endpoint http://localhost:3000/posts/1
Scenario Outline: T23431 - Validate API response using given endpoint - GET
	Given I have an endpoint /posts/1
	When I call the Get method of API
	Then I get API response as statuscode as <expectedResult>
Examples: Test Data
| expectedResult |
| OK			 |


@TestRegression
#Testing endpoint http://localhost:3000/posts/userid
Scenario Outline: T23432 - Get user information using userid - GET
	Given I have an endpoint /posts/
	When I call the method GET to fetch user information using the ID <userid>
	Then I will get user information as like as expected file <expectedResultPath>

Examples: User Info and path of the expected result
| userid | expectedResultPath                           |
| 2      | @"Json\Response\customer-data-response.json" |

@TestRegression
#Testing endpoint http://localhost:3000/userinformation?userId=101&account=4534
Scenario Outline: T23433 - Get user account information using userid and accountnumber - GET
	Given I have an endpoint /userinformation/
	When I call the method GET to fetch user information using the <userId> and <accountNumber>
	Then I will get the response statuscode as <expectedResult>

Examples: User info plus account info and expected result
| userId | accountNumber | expectedResult |
| 101    | 4534          | OK            |


@TestIntegs @TestRegression
#Testing endpoint http://localhost:3000/posts
Scenario Outline: T23434 - Post book information - POST
	Given I have an endpoint /posts/
	When I call a POST method with the request <jsonRequestFilePath> to register a book
	Then I will register the book successfuly returning statuscode as <expectedResult>
Examples: Test data expected result
| expectedResult | jsonRequestFilePath                    |
| created        | @"Json\Request\book-data-request.json" |


@TestRegression
#Testing endpoint http://localhost:3000/posts/bookId
Scenario Outline: T23435 - Validate json schema response for book registered - GET
	Given I have an endpoint /posts/
	When I call a GET method to retrieve a book information with its <bookId>
	Then I will get the book information as json to validate jsonschema as <expectedResultPath>
Examples: Book info plus expected result path
| bookId | expectedResultPath                     |
| 3      | @"Json\Schema\book-data-schema.json" |