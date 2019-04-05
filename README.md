# DDD Fundamentals Sample App

A sample meant to demonstrate domain driven design using a veterinary hospital management system. This sample is used in the [Domain-Driven Design Fundamentals course on Pluralsight](https://app.pluralsight.com/library/courses/domain-driven-design-fundamentals).

## Give a Star! :star:
If you like or are using this project to learn, please give it a star. Thanks!

## Getting Started

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


