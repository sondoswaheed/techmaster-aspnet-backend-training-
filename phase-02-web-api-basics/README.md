# Phase 02 - Web API Basics

## Overview

Phase 02 focuses on building real ASP.NET Core Web APIs and understanding the fundamentals of HTTP communication, routing, controllers, DTOs, services, validation, and HTTP status codes.

The APIs in this phase use in-memory data where required. Database and Entity Framework Core work are not part of this phase.

---

## Objectives

By completing this phase, I practiced how to:

* Build ASP.NET Core Web APIs using Controllers.
* Design clear API routes.
* Use route parameters and query string parameters.
* Receive JSON request bodies using DTOs.
* Validate incoming requests.
* Return appropriate HTTP status codes.
* Separate business logic into services when needed.
* Implement basic CRUD operations.
* Search data using query parameters.
* Test APIs using Swagger/OpenAPI.
* Use Git commits to track progressive API development.

---

## Project Structure

```text
phase-02-web-api-basics/
│
├── README.md
│
├── task-00-api-setup/
│
├── task-01-rest-routing-drills/
│
├── task-02-student-management-api/
│
├── task-03-products-categories-api/
│
├── task-04-book-store-api/
│
├── task-05-postman-swagger-evidence/
│
├── task-06-api-standards-refactor-pack/
│
└── task-07-interview-answers/
```


# Testing

The API endpoints are tested using Swagger/OpenAPI.

Swagger provides an interactive interface where requests can be executed and responses and HTTP status codes can be verified.

Run the project locally and open:

```text
https://localhost:7064/swagger
```

---

# Phase 02 Learning Outcome

This phase provides practical experience with building and testing ASP.NET Core Web APIs before moving to database and Entity Framework Core development.

The main focus is understanding how requests enter an API, how data is received through routes, query strings and request bodies, how controllers process requests, how services handle business logic, and how appropriate HTTP responses are returned.

