Requirements:
0. Item has Id, Name, Price.
1. It has to be a Restful CRUD
2. Follow Controller, Service, Repository (connect to Postgre). (DbUp not required).
3. Use Dependency Injection.
4. Implement additional action "Buy", where quantity is required. If quantity is more than 10, apply 10% discount.
5. If it is more than 20 apply 20% discount.



Perdarymui

Additional requirements:
1. Replace Your Dapper repository with EF Core InMemory Repository.
2. Your service should  (maybe?) be interchangable with both Dapper and EF Core InMemory.
3. Update your application with DTO/Entity concepts.
Advanced:
1. Connect EF Core to Postgre. (Investigate Migrations)
2. try to  use AutoMapper to map entities to dtos.
https://github.com/JauniusPinelis/.NET-Academy-2023-Adform


Homework:
	
Use Fluent Assertions. Add Mock Verify to your tests. Use Autofixture for data generation. Autofixture.xunit2 has AutoData attribute.
	
Add new User Controller which performs GET, GET By Id, and Create to https://jsonplaceholder.typicode.com/users
	
Cover GetById with a unit tests.
	
	Optional, advanced: Have basic data caching for the system (Fetch data and save into database)

