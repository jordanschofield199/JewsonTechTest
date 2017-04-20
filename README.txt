Developer Comments
The solution can be found under ~\JewsonTechTest\Jewson.RestService\Jewson.RestService.sln
I originally planned to have seperate solutions for front and back end.
All references should be restored from Nuget.

1)	I have created a static data repository however the idea would be that it inherits from a generic interface. 
	This means that if we want to swap out this repo for a SQL server repo it should be easily replaceable.

2)	I have created a common logging library that could be reused in any application. I have included this within the solution and as a project reference however
	this ideally would be versioned and wrapped up in a Nuget package.

3)	I have created a client library. This is essentially just a HTTP wrapper which again could be packaged and versioned and used by 1 or more front end applications.
	The WebApiClient.cs could act as an interface for multiple instances of 'clients'. For example if there was another Web API to consume i could create a replica
	of the JewsonApiClient following the usage of what it inherits.

4)	I have included Swagger as the API documentation. You can browse to the API url /swagger which will have a rich UI. For example http://localhost/Jewson.RestService/api/swagger

5)	I have used Unity for my IOC and DI. This is used in both the Web API and MVC application. In future i could write something that automatically registers types, particularly those
	hidden behind libraries that are pulled in via Nuget.

6)	I have used NUnit and NSubstitute for my unit test framework and mocking.

7)	Some of the integration tests rely on having a service already deployed and the URL is configured within the APP.config

8)	The business layer has 1 simple data provider which i use to interact with the data layer and map objects into DTOs. I have chosen to use Automapper for mapping.

9)	Demonstrable through the front end i have implemented some server side paging using DataTables.

10)	I have chosen to use the MVC HtmlHelpers for Ajax.BeginForm rather than writing my own Ajax call in JS. I could have done this to demonstrate my JS skills however it would have meant
	writing more code than needed.

11) I didnt feel it was necessary to go over the top in regards to the front end therefore i just used Bootstrap for some basic layout and styling.

12) Both applications are configured to use Local IIS on port 80 with the following URLs http://localhost/Jewson.RestService & http://localhost/Jewson.FrontEnd