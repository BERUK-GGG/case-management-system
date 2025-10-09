# RB Case Management System

A modern case management system built with .NET 8 Web API backend and React.js frontend, fully containerized with Docker.

## Architecture Overview

- **Frontend**: React.js with Material-UI, served by Nginx
- **Backend**: .NET 8 Web API with Entity Framework Core
- **Database**: SQL Server 2022
- **Deployment**: Docker & Docker Compose

## Features

- Customer Management (CRUD operations)
- Appointment/Booking Management
- Service Journal Records
- Parts Inventory Management
- Mechanic Assignment
- RESTful API with Swagger documentation
- Responsive web interface
- Cross-Origin Resource Sharing (CORS) enabled

## Prerequisites

- Docker Desktop
- Docker Compose
- Git

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd case-management-system
   ```

2. **Start the application**
   ```bash
   docker-compose up -d
   ```

3. **Access the application**
   - Frontend: http://localhost:3000
   - API: http://localhost:5001
   - API Documentation: http://localhost:5001/swagger

## Project Structure

```
case-management-system/
├── RB_CaseManagement.API/          # .NET Web API
├── DataLayer/                      # Entity Framework & Repository Pattern
├── Entities/                       # Domain Models
├── Affärslager/                    # Business Logic Layer
├── rb-case-management-frontend/    # React.js Frontend
├── docker-compose.yml              # Multi-container orchestration
├── Dockerfile (API)                # Backend container definition
├── rb-case-management-frontend/Dockerfile  # Frontend container definition
└── README.md
```

## Services

### Frontend Service (rb-frontend)
- **Port**: 3000
- **Technology**: React.js + Nginx
- **Features**: 
  - Customer management interface
  - Booking system
  - Service records
  - Parts inventory
  - Responsive design with Material-UI

### API Service (rb-api)
- **Port**: 5001
- **Technology**: .NET 8 Web API
- **Features**:
  - RESTful endpoints
  - Entity Framework Core
  - Repository pattern
  - Swagger/OpenAPI documentation
  - CORS enabled for frontend integration

### Database Service (rb-sqlserver)
- **Port**: 1433 (internal)
- **Technology**: SQL Server 2022
- **Features**:
  - Persistent data storage
  - Automatic database creation
  - Test data seeding

## API Endpoints

### Customers
- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Bookings
- `GET /api/bookings` - Get all bookings
- `GET /api/bookings/{id}` - Get booking by ID
- `POST /api/bookings` - Create new booking
- `PUT /api/bookings/{id}` - Update booking
- `DELETE /api/bookings/{id}` - Delete booking

### Journals
- `GET /api/journals` - Get all service journals
- `GET /api/journals/{id}` - Get journal by ID
- `POST /api/journals` - Create new journal
- `PUT /api/journals/{id}` - Update journal
- `DELETE /api/journals/{id}` - Delete journal

### Parts
- `GET /api/parts` - Get all parts
- `GET /api/parts/{id}` - Get part by ID
- `POST /api/parts` - Create new part
- `PUT /api/parts/{id}` - Update part
- `DELETE /api/parts/{id}` - Delete part

### Mechanics
- `GET /api/mechanics` - Get all mechanics
- `GET /api/mechanics/{id}` - Get mechanic by ID
- `POST /api/mechanics` - Create new mechanic
- `PUT /api/mechanics/{id}` - Update mechanic
- `DELETE /api/mechanics/{id}` - Delete mechanic

## Development

### Running in Development Mode

1. **Start only the database**
   ```bash
   docker-compose up rb-sqlserver -d
   ```

2. **Run API locally**
   ```bash
   cd RB_CaseManagement.API
   dotnet run
   ```

3. **Run Frontend locally**
   ```bash
   cd rb-case-management-frontend
   npm install
   npm start
   ```

### Building Individual Services

**Build API**
```bash
docker build -t case-management-api .
```

**Build Frontend**
```bash
cd rb-case-management-frontend
docker build -t case-management-frontend .
```

## Docker Configuration

### docker-compose.yml
- Orchestrates 3 services: frontend, api, database
- Sets up internal networking
- Configures environment variables
- Maps ports for external access

### Frontend Dockerfile
- Multi-stage build: Node.js build + Nginx serve
- Optimized production build
- Custom nginx configuration for SPA routing

### API Dockerfile
- Multi-stage build: SDK build + Runtime serve
- Publishes optimized release build
- Exposes port 80 internally

## Environment Variables

### API Service
- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__DefaultConnection` - Database connection string

### Database Service
- `SA_PASSWORD=YourStrong@Passw0rd` - SQL Server SA password
- `ACCEPT_EULA=Y` - Accept SQL Server license

## Networking

All services run on a custom Docker network (`rb-network`) enabling:
- Service-to-service communication using container names
- Isolated network environment
- Automatic DNS resolution between containers

## Volumes

- **Database Volume**: Persists SQL Server data between container restarts
- **Location**: `sqlserver_data:/var/opt/mssql`

## Monitoring & Logs

**View logs for all services**
```bash
docker-compose logs -f
```

**View logs for specific service**
```bash
docker-compose logs -f rb-api
docker-compose logs -f rb-frontend
docker-compose logs -f rb-sqlserver
```

## Troubleshooting

### Container Issues
```bash
# Check container status
docker-compose ps

# Restart specific service
docker-compose restart rb-api

# Rebuild and restart
docker-compose up --build -d
```

### Database Connection Issues
```bash
# Check database container logs
docker-compose logs rb-sqlserver

# Test database connection
docker exec -it rb-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd'
```

### Frontend Issues
```bash
# Check nginx configuration
docker exec -it rb-frontend cat /etc/nginx/conf.d/default.conf

# Check built files
docker exec -it rb-frontend ls -la /usr/share/nginx/html
```

## Production Deployment

1. **Update environment variables** in docker-compose.yml
2. **Set production database password**
3. **Configure reverse proxy** (nginx/Apache) if needed
4. **Set up SSL certificates**
5. **Configure backup strategy** for database

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request


