# Limedika WebApp

The project is built to show basic communication between client and server sides using ASP.NET 6 framework.

### Features

The web application has three main features.

- **Import Clients:** All the clients are imported to database from separate JSON file which is placed inside project.
- **Update Post Codes:** Post code is updated based on address for each client separately by requesting external API.
- **Clients Overview:** All the clients imported to database will be shown in a table format.

## Getting Started

- **Prerequisites**:
    - Visual Studio
    - ASP.NET
    - MS SQL Server Express
    
## Configuration

- **Database**: Before running the project first setup the database with command *dotnet ef database update*
- **Connection strings**:
    - *External API:* Placed in WebUI appsettings.json file.
    - *Database:* Placed in WebAPI appsettings.json file.
