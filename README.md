# DDD Fundamentals Sample App

A sample meant to demonstrate domain driven design using a veterinary hospital management system. This sample is used in the [Domain-Driven Design Fundamentals course on Pluralsight](https://app.pluralsight.com/library/courses/domain-driven-design-fundamentals) .NET 5 update.

## Give a Star! :star:
If you like or are using this project to learn, please give it a star. Thanks!

## Getting Started

### Run In Docker
- Go inside ddd-vet-sample directory.
- Run these 2 commands
```powershell
docker-compose build
docker-compose up
```

- http://localhost:5103/ is front desk Api 
- http://localhost:5403/ is front desk Blazor
- http://localhost:5503/ is VetClinicPublic 
- localhost,1433 is MSSQL-2019 with front desk db schema inside.

If you want to change ports you have to change docker-compose.override.yml and appsettings as well.
```
ddd-vet-sample\docker-compose.override.yml
ddd-vet-sample\FrontDesk\src\FrontDesk.Api\appsettings.Docker.json
ddd-vet-sample\FrontDesk\src\FrontDesk.Blazor\wwwwroot\appsettings.Docker.json
```

### Run In IIS

- https://localhost:5101/ is front desk Api 
- https://localhost:5401/ is front desk Blazor
- https://localhost:5501/ is VetClinicPublic 

If you want to change ports you have to change appsettings.
```
ddd-vet-sample\FrontDesk\src\FrontDesk.Api\appsettings.Development.json
ddd-vet-sample\FrontDesk\src\FrontDesk.Blazor\wwwwroot\appsettings.Development.json
```


The main application is in the FrontDeskSolution folder. Open the FrontDesk.sln file for the main sample.

The application relies on the public web site for sending emails and confirming appointments. You must run the public site for this part of the demo to work. Open it from VetClinicPublic.Web folder and the VetClinicPublic.Web.sln solution.

You will need a test mail server to capture the email that would be sent to the user when they create a new appointment. I recommend SMTP4Dev which you can get here - I'm still using the [old version](https://github.com/rnwood/smtp4dev/releases?after=3.0.264-master) but the [latest one probably works, too](https://github.com/rnwood/smtp4dev/releases).

The communication between the two web apps is done using SQL Server Service Broker. You must set this up using the [SetupSQLServiceBroker.sql](./SetupSQLServiceBroker.sql) file in the root of the repo.

Finally, if you have to adjust the connection strings for the database to use your version of LocalDB (or SQL Express or whatever) you will need to update these in several places:

* FrontDesk web.config
* FrontDesk SharedDatabaseManagementTools SharedDatabaseTests app.config
* VetClinicPublic web.config
* MessagingConfig.cs in both solutions

Basically you should search both solutions for `(LocalDb)\MSSQLLocalDB` and replace it with whatever local SQL Server database you're using.

To create the application/domain database you should run the unit test in SharedDatabaseTests - `SharedDatabaseContextShould.BuildModel`

The message queue database is created by the SetupSQLServiceBroker script above, and is called `ServiceBrokerTest`. 

## Community Contributors

Thanks for our community contributors:

* [ShadyNagy](https://github.com/ShadyNagy)
