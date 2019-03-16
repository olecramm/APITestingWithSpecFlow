Feature: MyTestsScenarios

@TestIntegs
#http://localhost:3000/posts/1
Scenario: T23431 - Validate API response using given endpoint - GET
	Given I have an endpoint /posts/1
	When I call the Get method of API
	Then I get API response as statuscode as 200OK

#http://localhost:3000/posts/userid
Scenario Outline: T23432 - Get user information using userid - GET
	Given I have an endpoint /posts/
	When I call the method GET to fetch user information using the ID <userid>
	Then I will get user information

Examples: User Info
| userid |
| 2      |

#http://localhost:3000/userinformation?userId=101&account=4534
Scenario Outline: T23433 - Get user account information using userid and accountnumber - GET
	Given I have an endpoint /userinformation/
	When I call the method GET to fetch user information using the <userId> and <accountNumber>
	Then I will get the user information

Examples: User info
| userId | accountNumber |
| 101    | 4534        |

#http://localhost:3000/posts
Scenario: T23434 - Post book information
	Given I have an endpoint /posts/
	When I call a POST method to register a book
	Then I will register the book successfuly

#http://localhost:3000/posts/bookId
Scenario Outline: T23435 - Validate json schema response for book registered
	Given I have an endpoint /posts/
	When I call a GET method to retrieve a book information with its <bookId>
	Then I will get the book information for validation
Examples: 
| bookId |
| 3		 |