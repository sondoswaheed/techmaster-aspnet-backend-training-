# Task 01 - REST Routing Drills

## Overview

This task contains a collection of practical ASP.NET Core Web API drills designed to build a strong understanding of HTTP APIs, routing, request parameters, DTOs, validation, CRUD operations, searching, pagination, headers, status codes, and standard error responses.

All drills use in-memory data where required. No database or Entity Framework Core is used in this task.

---

## Objectives

By completing these drills, I practiced how to:

* Create ASP.NET Core API controllers.
* Design RESTful routes.
* Use route parameters.
* Use query string parameters.
* Read HTTP request headers.
* Receive JSON request bodies.
* Create and use DTOs.
* Validate incoming requests.
* Return appropriate HTTP status codes.
* Implement CRUD operations.
* Search collections using LINQ.
* Implement pagination using `Skip()` and `Take()`.
* Understand Dependency Injection and service-based business logic.
* Create consistent error response structures.
* Test API endpoints using Swagger/OpenAPI.

---

# Drills

## Drill 01 - Health Check Endpoint

### Endpoint

```http
GET /api/health
```

### Purpose

Checks whether the API is running and reachable.

### Response

```json
{
  "status": "Running",
  "service": "TechMaster API",
  "time": "2026-08-25T00:00:00Z"
}
```

### Status Code

```text
200 OK
```

---

## Drill 02 - Route Parameter Echo

### Endpoint

```http
GET /api/tools/echo/{name}
```

### Example

```http
GET /api/tools/echo/Mohamed
```

### Response

```json
{
  "originalName": "Mohamed",
  "message": "Hello, Mohamed!"
}
```

### Concept

Route parameters.

---

## Drill 03 - Query String Calculator

### Endpoint

```http
GET /api/calculator/add?a=10&b=5
```

### Response

```json
{
  "a": 10,
  "b": 5,
  "operation": "addition",
  "result": 15
}
```

### Concept

Query string parameters using `[FromQuery]`.

---

## Drill 04 - Temperature Conversion API

### Endpoint

```http
GET /api/converter/celsius-to-fahrenheit?value=25
```

### Purpose

Converts a Celsius temperature to Fahrenheit.

### Example

```json
{
  "celsius": 25,
  "fahrenheit": 77,
  "formulaUsed": "(Celsius × 9 / 5) + 32"
}
```

### Concept

Business calculation and service-based logic.

The calculation is handled by `ConverterService` instead of being placed directly inside the controller.

---

## Drill 05 - Grade API

### Endpoint

```http
GET /api/grades/calculate?score=85
```

### Example Response

```json
{
  "score": 85,
  "grade": "B",
  "passed": true
}
```

### Validation

The score must be between `0` and `100`.

Examples:

```text
score=85   → 200 OK
score=100  → 200 OK
score=-5   → 400 Bad Request
score=120  → 400 Bad Request
```

### Concept

Validation and conditional business logic.

---

# Notes API

Drills 06-12 use an in-memory Notes collection.

The Notes API demonstrates basic CRUD operations, searching, and pagination without using a database.

---

## Drill 06 - Create Note

### Endpoint

```http
POST /api/notes
```

### Request Body

```json
{
  "title": "My First Note",
  "content": "Learning ASP.NET Core Web API"
}
```

### Response

A created note containing:

* `id`
* `title`
* `content`
* `createdAt`

### Status Code

```text
201 Created
```

### Validation

`title` is required.

---

## Drill 07 - Get Notes List

### Endpoint

```http
GET /api/notes
```

### Purpose

Returns all notes stored in memory.

If no notes exist:

```json
[]
```

### Concept

Collection responses.

---

## Drill 08 - Get Note By ID

### Endpoint

```http
GET /api/notes/{id}
```

### Example

```http
GET /api/notes/1
```

### Possible Responses

Existing note:

```text
200 OK
```

Non-existing note:

```text
404 Not Found
```

---

## Drill 09 - Update Note

### Endpoint

```http
PUT /api/notes/{id}
```

### Request Body

```json
{
  "title": "Updated Note",
  "content": "Updated content"
}
```

### Validation

Both `title` and `content` are required.

### Possible Responses

```text
200 OK
400 Bad Request
404 Not Found
```

The existing note is updated rather than creating a new note.

---

## Drill 10 - Delete Note

### Endpoint

```http
DELETE /api/notes/{id}
```

### Possible Responses

Successful deletion:

```text
204 No Content
```

Note does not exist:

```text
404 Not Found
```

---

## Drill 11 - Search Notes

### Endpoint

```http
GET /api/notes/search?keyword=api
```

### Purpose

Searches notes by both title and content.

The search is case-insensitive.

### Example

```http
GET /api/notes/search?keyword=api
```

Matching notes are returned as a JSON collection.

An empty keyword returns:

```text
400 Bad Request
```

### Concept

Query parameters and LINQ filtering.

---

## Drill 12 - Pagination Demo

### Endpoint

```http
GET /api/notes?pageNumber=1&pageSize=5
```

### Validation

`pageNumber` must be greater than `0`.

`pageSize` must be between `1` and `50`.

### Pagination Formula

```text
Skip = (pageNumber - 1) * pageSize
```

The implementation uses:

```csharp
Skip()
Take()
```

### Response

```json
{
  "items": [],
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 0
}
```

### Examples

```text
pageNumber=1&pageSize=5 → first 5 notes
pageNumber=0            → 400 Bad Request
pageSize=100            → 400 Bad Request
```

---

# Drill 13 - Header Reader Endpoint

### Endpoint

```http
GET /api/request-info
```

### Required Header

```text
X-Student-Name: Sondos
```

### Response

```json
{
  "studentName": "Sondos",
  "requestPath": "/api/request-info"
}
```

### Validation

If the `X-Student-Name` header is missing:

```text
400 Bad Request
```

### Concept

Reading custom HTTP request headers.

---

# Drill 14 - Status Code Practice

This drill demonstrates common HTTP status codes through simple API use cases.

| Method | Endpoint                        | Status Code     |
| ------ | ------------------------------- | --------------- |
| GET    | `/api/status/health`            | 200 OK          |
| POST   | `/api/status/create`            | 201 Created     |
| DELETE | `/api/status/delete`            | 204 No Content  |
| GET    | `/api/status/validate?value=-1` | 400 Bad Request |
| GET    | `/api/status/missing`           | 404 Not Found   |

### Concepts

* `Ok()`
* `StatusCode(201, ...)`
* `NoContent()`
* `BadRequest()`
* `NotFound()`

---

# Drill 15 - Standard Error Shape

### Endpoint

```http
GET /api/errors/demo
```

The endpoint demonstrates a consistent structure for API error responses.

### Bad Request Example

```http
GET /api/errors/demo?type=bad-request
```

Response:

```json
{
  "success": false,
  "message": "Invalid request",
  "code": "VALIDATION_ERROR",
  "details": [
    "Name is required"
  ]
}
```

Status:

```text
400 Bad Request
```

### Not Found Example

```http
GET /api/errors/demo?type=not-found
```

Response:

```json
{
  "success": false,
  "message": "Resource not found",
  "code": "NOT_FOUND",
  "details": [
    "The requested resource does not exist"
  ]
}
```

Status:

```text
404 Not Found
```

---

# Main Concepts Covered

| Concept                    | Drills                 |
| -------------------------- | ---------------------- |
| Controllers                | 01-15                  |
| Basic GET endpoint         | 01                     |
| Route parameters           | 02, 08-10              |
| Query parameters           | 03, 05, 11, 12         |
| Request body               | 06, 09                 |
| DTOs                       | 06, 09                 |
| Validation                 | 02, 05, 06, 09, 12, 13 |
| Services                   | 04                     |
| Dependency Injection       | 04                     |
| CRUD                       | 06-10                  |
| LINQ filtering             | 08, 11                 |
| Pagination                 | 12                     |
| Request headers            | 13                     |
| HTTP status codes          | 14                     |
| Error response consistency | 15                     |
| Swagger/OpenAPI testing    | 01-15                  |

---

# HTTP Status Codes Practiced

| Status Code       | Meaning                                    | Used In           |
| ----------------- | ------------------------------------------ | ----------------- |
| `200 OK`          | Request succeeded                          | GET, PUT          |
| `201 Created`     | Resource created                           | POST              |
| `204 No Content`  | Successful operation without response body | DELETE            |
| `400 Bad Request` | Invalid request                            | Validation        |
| `404 Not Found`   | Requested resource does not exist          | Get/Delete/Errors |

---

# Testing

The endpoints can be tested using Swagger/OpenAPI.

Swagger allows requests to be executed directly from the browser and provides a clear view of:

* HTTP methods
* Routes
* Query parameters
* Route parameters
* Request bodies
* Response bodies
* HTTP status codes

Custom headers such as `X-Student-Name` can also be tested using an API testing tool such as Postman.

---

# Data Storage

The Notes API uses a static in-memory list for demonstration purposes.

```csharp
private static readonly List<NoteResponse> Notes = new();
```

This means data is temporary and will be lost when the application stops.

No database or Entity Framework Core is used in these drills.

---

# Expected Outcome

After completing Task 01, I should be able to explain and implement:

1. How an HTTP request reaches a controller.
2. The difference between route parameters and query parameters.
3. How to receive JSON request bodies.
4. Why DTOs are used for API requests.
5. How validation works.
6. How CRUD endpoints are designed.
7. How LINQ can be used to search and paginate data.
8. How to read HTTP request headers.
9. Why different HTTP status codes are used.
10. How to return a consistent error response structure.

---

## Task Status

**Task 01 - REST Routing Drills: Completed**

The task contains 15 API drills covering the fundamental concepts required before moving to larger Web API projects.
