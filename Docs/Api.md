# BuberDinner API Documentation

## Overview
BuberDinner is a backend API solution for a dinner management system. This document provides comprehensive information about the API endpoints, authentication, and usage patterns.

## Table of Contents
- [Getting Started](#getting-started)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
- [Error Handling](#error-handling)
- [Response Format](#response-format)

## Getting Started

### Prerequisites
- .NET Core 6.0 or higher
- SQL Server or compatible database
- API client (Postman, curl, etc.)

### Environment Setup
Configure your development environment by setting up the necessary connection strings and API credentials in your configuration files.

## Authentication

The API uses JWT (JSON Web Token) authentication for secure access to protected endpoints.

### Authentication Flow
1. User submits credentials to login endpoint
2. Server returns JWT token
3. Client includes token in Authorization header for subsequent requests
4. Token is validated on each protected endpoint

### Header Format
```
Authorization: Bearer {token}
```

## API Endpoints

### User Management
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and receive JWT token
- `GET /api/users/{id}` - Get user details
- `PUT /api/users/{id}` - Update user information

### Dinner Management
- `GET /api/dinners` - List all dinners
- `GET /api/dinners/{id}` - Get dinner details
- `POST /api/dinners` - Create a new dinner
- `PUT /api/dinners/{id}` - Update dinner details
- `DELETE /api/dinners/{id}` - Delete a dinner

### Menu Management
- `GET /api/menus` - List all menus
- `GET /api/menus/{id}` - Get menu details
- `POST /api/menus` - Create a new menu
- `PUT /api/menus/{id}` - Update menu

### Reservations
- `POST /api/dinners/{dinnerId}/reservations` - Create a reservation
- `GET /api/dinners/{dinnerId}/reservations` - List dinner reservations
- `DELETE /api/reservations/{id}` - Cancel a reservation

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

### Paginated Response
```json
{
  "data": [],
  "totalCount": 0,
  "pageNumber": 1,
  "pageSize": 10
}
```

## Development Guidelines

### Folder Structure
- `Controllers` - API endpoint handlers
- `Services` - Business logic layer
- `Models` - Data models and DTOs
- `Repositories` - Data access layer
- `Middleware` - Custom middleware components

### Best Practices
- Use dependency injection for loose coupling
- Implement proper error handling and logging
- Follow RESTful conventions
- Include comprehensive input validation
- Use DTOs for API responses
