# Quick Start Guide

## Getting Started with SchoolConnect Education System API

This guide will help you get the API running quickly using Docker Compose.

### Prerequisites

- [Docker](https://www.docker.com/get-started) and Docker Compose installed
- Or alternatively, .NET 10 SDK and MongoDB for local development

### Option 1: Quick Start with Docker Compose (Recommended)

1. Clone the repository:
   ```bash
   git clone https://github.com/dirkdhure365/schoolconnect.git
   cd schoolconnect
   ```

2. Start the application:
   ```bash
   docker-compose up --build
   ```

3. Access the API:
   - Swagger UI: http://localhost:5000
   - API Base URL: http://localhost:5000/api
   - MongoDB: mongodb://localhost:27017

4. Seed the database with sample data:
   - Open Swagger UI at http://localhost:5000
   - Find the `POST /api/seed` endpoint under "Seed Data"
   - Click "Try it out" and then "Execute"
   - This will populate the database with sample data for all four Southern African countries

5. Explore the API:
   - Use Swagger UI to interact with all endpoints
   - Try querying countries: `GET /api/countries`
   - Browse education systems, programs, and subjects

### Option 2: Running Locally without Docker

1. Install MongoDB locally or use a cloud instance

2. Update `appsettings.json` with your MongoDB connection string:
   ```json
   {
     "MongoDB": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "EducationSystemDb"
     }
   }
   ```

3. Run the application:
   ```bash
   cd src/SchoolConnect.EducationSystem.Api
   dotnet run
   ```

4. Access at http://localhost:5000

### API Endpoints Overview

- **Countries**: `/api/countries` - Manage Southern African countries
- **Education Systems**: `/api/education-systems` - National education frameworks
- **Assessment Boards**: `/api/assessment-boards` - Examination bodies (ZIMSEC, DBE, BEC, ECZ)
- **Education Phases**: `/api/education-phases` - Foundation, Intermediate, Senior, FET phases
- **Programs**: `/api/programs` - O Level, A Level, NSC, BGCSE qualifications
- **Subjects**: `/api/subjects` - Academic subjects per program
- **Curricula**: `/api/curricula` - Detailed curriculum content
- **Events**: `/api/events` - Event sourcing audit trail
- **Seed Data**: `/api/seed` - Populate database with sample data

### Sample Workflow

1. **Seed the database**:
   ```
   POST /api/seed
   ```

2. **Get all countries**:
   ```
   GET /api/countries
   ```

3. **Get education systems for Zimbabwe**:
   ```
   GET /api/countries/{zimbabwe-id}/education-systems
   ```

4. **Get programs for ZIMSEC**:
   ```
   GET /api/assessment-boards/{zimsec-id}/programs
   ```

5. **Get subjects for O Level program**:
   ```
   GET /api/programs/{o-level-id}/subjects
   ```

### Sample Data Included

The seed data includes:

**Zimbabwe (ZIMSEC)**
- O Level: 13 subjects including English, Mathematics, Sciences, Commerce
- A Level: 11 subjects including advanced Mathematics, Sciences, Business Studies

**South Africa (CAPS/DBE)**
- Foundation Phase (Grade R-3): 4 core subjects
- Intermediate Phase (Grade 4-6): 6 subjects
- Senior Phase (Grade 7-9): 9 subjects
- FET/NSC (Grade 10-12): 14 subjects including choice subjects

**Botswana (BEC)**
- Junior Certificate: Forms 1-3
- BGCSE: 9 subjects including English, Setswana, Mathematics, Sciences

**Zambia (ECZ)**
- Junior Secondary (Grade 8-9)
- School Certificate (Grade 10-12): 11 subjects

### Event Sourcing

All changes are tracked as events. Query the event store:
- Get all events: `GET /api/events`
- Get events for a specific entity: `GET /api/events/{aggregate-id}`

### Stopping the Application

With Docker Compose:
```bash
docker-compose down
```

To also remove volumes (database data):
```bash
docker-compose down -v
```

### Troubleshooting

**MongoDB connection issues:**
- Ensure MongoDB is running
- Check connection string in configuration
- For Docker: Ensure containers are on the same network

**Port conflicts:**
- Change ports in `docker-compose.yml` if 5000/5001/27017 are in use

**Build errors:**
- Ensure .NET 10 SDK is installed
- Run `dotnet restore` to restore packages
- Clear build artifacts: `dotnet clean`

### Next Steps

- Explore the Swagger UI documentation
- Review the comprehensive data model
- Test CQRS commands and queries
- Examine event sourcing capabilities
- Extend with your own education system data

For more information, see the main [README.md](README.md).
