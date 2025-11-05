# Employee API Application

A simple ASP.NET Core Web API for managing employee data with CSV export functionality.

## Features

- CRUD operations for employee management
- CSV export functionality for employee data
- In-memory database for quick testing
- Swagger/OpenAPI documentation
- Weather forecast endpoint (example)

The API will be available at `https://localhost:7169` or `http://localhost:4446`

### Accessing Swagger UI

Once the application is running, you can access the Swagger UI at:
- `http://localhost:4446/swagger` (HTTP)

## API Endpoints

### Employee Management

- `GET /api/Employees/View All Employees` - Get all employees
- `GET /api/Employees/View Specific Employee?id={id}` - Get a specific employee by ID
- `POST /api/Employees/Add New Employee` - Add a new employee
- `PUT /api/Employees/Update Employee?id={id}` - Update an existing employee
- `DELETE /api/Employees/Remove Employee?id={id}` - Delete an employee
- `GET /api/Employees/Download CSV` - Download all employees as CSV file

### Weather Forecast

- `GET /WeatherForecast?date={date}&temperatureC={temp}&city={city}` - Get weather forecast