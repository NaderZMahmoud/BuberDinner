# BuberDinner API Documentation

## Overview
BuberDinner is a backend API solution for a dinner management system, currently in development. This document provides comprehensive information about the API endpoints, authentication, and usage patterns.

## Table of Contents
- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Response Format](#response-format)

## Getting Started

### Prerequisites
- .NET 10.0 or higher
- API client (Postman, curl, etc.)

### Environment Setup
Configure your development environment. The application uses dependency injection and is structured in a clean architecture pattern.

### Running the Application
1. Navigate to the `BuberDinner.Api` directory
2. Run `dotnet run`
3. The API will be available at `https://localhost:5073` (or the port specified in launchSettings.json)

## Architecture

The solution follows Clean Architecture principles with the following layers:

- **BuberDinner.Api**: ASP.NET Core Web API project containing controllers and OpenAPI configuration
- **BuberDinner.Application**: Application services and business logic
- **BuberDinner.Contracts**: Request/Response DTOs and contracts
- **BuberDinner.Domain**: Domain entities and business rules (currently empty)
- **BuberDinner.InfraStructure**: Infrastructure concerns like data access and external services (minimal implementation)

## Authentication

The API currently implements basic authentication endpoints. JWT authentication is planned but not yet implemented - the service returns mock tokens.

### Authentication Flow
1. User submits credentials to login endpoint
2. Server returns mock authentication result with dummy token
3. Client receives authentication response

### Header Format (Planned)
```
Authorization: Bearer {token}
```

## API Endpoints

### Authentication
- `POST /auth/register` - Register a new user (mock implementation)
- `POST /auth/login` - Login and receive mock authentication result

### Weather Forecast
- `GET /weatherforecast` - Get sample weather forecast data (for testing)

### Request Examples

#### Register User
```http
POST /auth/register
Content-Type: application/json

{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "password": "Password123!",
    "confirmPassword": "Password123!"
}
```

#### Login User
```http
POST /auth/login
Content-Type: application/json

{
    "email": "john.doe@example.com",
    "password": "Password123!"
}
```

#### Weather Forecast
```http
GET /weatherforecast
Accept: application/json
```

## Error Handling

The API returns standard HTTP status codes and error messages in a consistent format.

### Common Status Codes
- `200 OK` - Request successful
- `201 Created` - Resource created successfully
- `400 Bad Request` - Invalid request parameters
- `401 Unauthorized` - Authentication required or failed
- `403 Forbidden` - Access denied
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

### Error Response Format
```json
{
  "code": "ERROR_CODE",
  "message": "Human readable error message",
  "details": {}
}
```

## Response Format

### Success Response
```json
{
  "data": {},
  "message": "Success"
}
```

### Authentication Response
```json
{
  "id": "guid",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "token": "string"
}
```

### Weather Forecast Response
```json
[
  {
    "date": "2024-01-01",
    "temperatureC": 25,
    "summary": "Warm"
  }
]
```

## Development Guidelines

### Folder Structure
- `Controllers` - API endpoint handlers
- `Services` - Business logic layer
- `Models` - Data models and DTOs (Contracts project)
- `DependencyInjection` - Service registration

### Best Practices
- Use dependency injection for loose coupling
- Implement proper error handling and logging
- Follow RESTful conventions
- Include comprehensive input validation
- Use DTOs for API responses

### Current Status
- Authentication endpoints implemented with mock service
- Weather forecast endpoint for testing
- Clean architecture structure in place
- OpenAPI/Swagger enabled for API documentation
- Domain and Infrastructure layers ready for implementation
