# JobBoardAPI

JobBoardAPI is a RESTful API for managing job postings, applications, companies, and applicants. Built with ASP.NET Core, it provides endpoints for job boards and related operations.

This project uses a monolithic architecture approach.

## Features
- Manage jobs, companies, applicants, and applications
- User authentication and registration
- JWT-based authentication
- Entity Framework Core for data access
- API endpoints for CRUD operations

## Project Structure
- `Controllers/` - API controllers for each entity
- `Models/` - Data models
- `Dtos/` - Data transfer objects
- `Services/` - Business logic and interfaces
- `Data/` - Entity Framework DbContext
- `Migrations/` - Database migrations
- `MQ/` - Message queue sender interface and implementation

## Getting Started
1. **Clone the repository**
   ```powershell
   git clone https://github.com/salahuddinuk/Job-Board-Api.git
   ```
2. **Navigate to the project directory**
   ```powershell
   cd JobBoardAPI/JobBoardAPI
   ```
3. **Restore dependencies**
   ```powershell
   dotnet restore
   ```
4. **Update database**
   ```powershell
   dotnet ef database update
   ```
5. **Run the API**
   ```powershell
   dotnet run
   ```

## Configuration
- Update `appsettings.json` and `appsettings.Development.json` for connection strings and settings.

## API Endpoints
Refer to the controllers in `Controllers/` for available endpoints.

## License
MIT
