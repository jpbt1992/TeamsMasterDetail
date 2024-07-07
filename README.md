# Project title

## Master-detail
Master-detail pages are quite common in many web applications, and there are several approaches to creating master-detail pages: using the server-side, client-side, and hybrid approach.

## About this application
An application to manage information about teams and their members using ASP.NET Core MVC and EF Core accessing the SQL Server database, where we will have two entities:

- **Team** - It is the main entity that contains team information such as TeamId, Name, Description. A team can have many members and Team is the Master or main entity.

- **Member** - This is the entity that contains information about the members or components of the team: MemberId, TeamId, Name and Email. Member is the details entity.

After ORM mapping and application of Migrations we will have two tables in the SQL Server database: Teams and Members using a one-to-many relationship.
