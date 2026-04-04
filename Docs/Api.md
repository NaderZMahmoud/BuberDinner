# BuberDinner API Documentation

## Overview
BuberDinner is a backend API solution for a dinner management system, currently in development. This document provides comprehensive information about the API endpoints, authentication, and usage patterns.

The solution is built using .NET 10.0 and follows Clean Architecture principles. The project uses the modern `.slnx` solution file format for simplified project management.

## Table of Contents
- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Response Format](#response-format)

## Getting Started

### Prerequisites
- .NET 10.0 SDK or higher
- API client (Postman, curl, etc.)

### Dependencies
The solution uses the following key packages:
- `Microsoft.AspNetCore.OpenApi` (10.0.5) - For OpenAPI/Swagger documentation
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` (10.0.2) - For code generation tools

### Environment Setup
Configure your development environment. The application uses dependency injection and is structured in a clean architecture pattern.

### Building the Solution
1. Ensure you have .NET 10.0 SDK installed
2. Run `dotnet build` from the solution root or `BuberDinner.Api` directory
3. The build should complete successfully with no errors

### Running the Application
1. Navigate to the `BuberDinner.Api` directory
2. Run `dotnet run`
3. The API will be available at `http://localhost:5073` (HTTP) or `https://localhost:7077` (HTTPS)
4. OpenAPI/Swagger documentation available at `http://localhost:5073/openapi/v1.json` or `https://localhost:7077/openapi/v1.json`

### Testing the API
Use the provided `.http` files in the `Requests` folder for testing:
- `Requests/Authentication/Register.http` - Test user registration
- `Requests/Authentication/Login.http` - Test user login

These files can be executed directly in VS Code or imported into tools like Postman.

## Architecture

The solution follows Clean Architecture principles with the following layers:

- **BuberDinner.Api**: ASP.NET Core Web API project containing controllers and OpenAPI configuration
- **BuberDinner.Application**: Application services and business logic (includes authentication service and interfaces)
- **BuberDinner.Contracts**: Request/Response DTOs and contracts
- **BuberDinner.Domain**: Domain entities including `User` and business rules
- **BuberDinner.InfraStructure**: Infrastructure concerns including:
  - JWT token generation with configuration-based secrets
  - User persistence repository
  - DateTime provider service
  - Database abstraction layer

## Authentication

The API implements JWT (JSON Web Token) authentication with user persistence.

### Authentication Flow
1. **Register**: User submits credentials to register a new account
   - Server validates that user doesn't already exist by email
   - Creates new `User` entity and persists to database
   - Generates JWT token signed with HS256
   - Returns user info with token
2. **Login**: User submits email and password
   - Server validates credentials against stored user
   - Generates new JWT token
   - Returns user info with token

### JWT Configuration
- Algorithm: HS256 (HMAC SHA-256)
- Secret: Configured in `appsettings.json` under `Jwt:Secret`
- Expiration: 1 day

### Header Format
```
Authorization: Bearer {token}
```

## API Endpoints

### Authentication
- `POST /auth/register` - Register a new user (mock implementation)
- `POST /auth/login` - Login and receive mock authentication result

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
- ✅ Authentication endpoints fully implemented (Register & Login)
- ✅ JWT token generation with HS256 signing
- ✅ User persistence with repository pattern
- ✅ User duplicate detection by email
- ✅ Clean architecture structure implemented
- ✅ OpenAPI/Swagger enabled for API documentation
- ✅ Domain entities implemented (User)
- ✅ Infrastructure layer with repositories and services
- ✅ Dependency injection fully configured

### Configuration
JWT settings can be configured in `appsettings.json`:
```json
{
  "Jwt": {
    "Secret": "your-secret-key-here",
    "Issuer": "BuberDinner",
    "Audience": "BuberDinner"
  }
}
```

**Important**: The secret must be at least 128 bits (16 characters) for HS256 algorithm.
