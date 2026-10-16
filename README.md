RaceDay Management System - Backend API (Part 2)

Project Description

This project is a backend Web API built using ASP.NET Core for managing race events, participant enrolments, categories, and event results. It includes user authentication and database persistence using Entity Framework Core.

Features

User authentication for Organisers, Participants, and Admins using BCrypt password hashing.

Event management to create, update, and fetch sporting events.

Category and enrolment management for race distances and registrations.

Results and leaderboard generation based on participant finish times.

Tech Stack

Framework: ASP.NET Core Web API (.NET 8)

Database Access: Entity Framework Core

Database: Microsoft SQL Server

Security: BCrypt.Net

Testing: xUnit

Prerequisites and Setup

Prerequisites
Install .NET SDK version 8.0 or higher, Microsoft SQL Server, and Visual Studio 2022.

Database Connection Configuration
Open appsettings.json in the project directory and update the connection string to point to your local SQL Server instance:

"ConnectionStrings": {
"DefaultConnection": "Server=YOUR_SERVER_NAME;Database=RaceDayDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

Database Update
Open your terminal in the project root folder and execute:

dotnet ef database update

Running the API
Execute the following command in your terminal:

dotnet run

The API will start locally at https://localhost:5001 or http://localhost:5000. You can access the Swagger UI documentation in your browser at /swagger.

API Endpoints

Authentication: POST /api/auth/register, POST /api/auth/login

Events: GET /api/events, POST /api/events

Categories: GET /api/events/{id}/categories, POST /api/events/{id}/categories

Enrolments: POST /api/events/{id}/enrolments

Results: GET /api/events/{id}/results, POST /api/events/{id}/results

