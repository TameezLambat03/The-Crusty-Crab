# The Crusty Crab

## Project Overview

The Crusty Crab is a restaurant management and ordering system developed as a full-stack software application.

The project consists of three integrated components:

- **MVC Web Application:** Provides the customer-facing website and management functionality.
- **ASP.NET Core Web API:** Handles backend functionality, data management, business logic and communication between application components.
- **Android Application:** Provides a mobile application that integrates with the restaurant system through the API.

The system supports restaurant operations including menu management, customer accounts, ordering, checkout, delivery addresses, customer reviews and rewards.

## Project Demonstration

Watch the Crusty Crab application demonstration:

[![Watch the Crusty Crab demonstration](https://img.youtube.com/vi/wARdxpGVP3A/0.jpg)](https://youtu.be/wARdxpGVP3A)

## System Architecture

The project uses a shared backend API to connect its applications.

- The MVC application communicates with the ASP.NET Core API.
- The Android application communicates with the ASP.NET Core API.
- The API handles application requests and interacts with the MySQL database.

## Technology Stack

| Component | Technologies |
|---|---|
| Web Application | ASP.NET Core MVC, C#, HTML, CSS, JavaScript |
| Backend API | ASP.NET Core Web API, C# |
| Mobile Application | Android, Kotlin |
| Database | MySQL |
| ORM | Entity Framework Core |
| Version Control | Git and GitHub |

## Repository Structure

The repository contains the following main components:

- **MVC Application:** Web interface, controllers, views, services and frontend styling.
- **CrustyAPI:** Backend API, database context, models, services and API controllers.
- **Android Application:** Native Android application developed using Kotlin.

## Getting Started

To work with the project, you will need:

- Visual Studio with ASP.NET and web development support.
- Android Studio for the Android application.
- .NET SDK compatible with the projects.
- MySQL Server and MySQL Workbench.
- Git.

### Database Configuration

The API requires a MySQL database connection.

Create an `appsettings.json` file in the `CrustyAPI` project directory and configure the `DefaultConnection` connection string using your own MySQL credentials.

Do not commit database passwords, API keys or other private configuration values to GitHub.

### Running the Applications

1. Configure and start the MySQL database.
2. Configure the API connection string.
3. Start the ASP.NET Core API.
4. Start the MVC application.
5. Open the Android project in Android Studio and configure its API connection.

## Development

The project is maintained using Git and GitHub.

The `main` branch contains the integrated project, including the MVC application, API and Android application.

## Project Contributors

Developed as part of the IIE MSA software development programme.