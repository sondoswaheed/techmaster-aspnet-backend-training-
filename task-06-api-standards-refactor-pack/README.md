# Task 06 - API Standards & Refactor Pack

## Original Problems

* Controller contained storage and business logic.
* POST endpoint used query/string parameters instead of a request body DTO.
* Invalid data returned `200 OK` with error text.
* Product used public fields instead of properties.
* No service layer existed.
* Routes were not following RESTful API conventions.
* GET for a missing product returned `200 OK` instead of `404 Not Found`.

## Improvements Made

* Created a `Product` model using properties instead of public fields.
* Added `CreateProductRequest` DTO for creating products.
* Added `ProductResponse` DTO for API responses.
* Created `IProductService` interface.
* Created `ProductService` to handle product storage, mapping, and business logic.
* Moved product logic out of the controller into the service layer.
* Used Data Annotations for request validation.
* Used `BadRequest (400)` for invalid request data.
* Used `NotFound (404)` when a product does not exist.
* Used `Created (201)` when creating a new product.
* Replaced bad route names with RESTful routes.

## API Endpoints

| Method | Endpoint             | Description          |
| ------ | -------------------- | -------------------- |
| POST   | `/api/products`      | Create a new product |
| GET    | `/api/products`      | Get all products     |
| GET    | `/api/products/{id}` | Get a product by ID  |

## Before vs After

### Before

The original API had most of the logic inside the controller.

* Products were stored directly in the controller.
* Product properties were public fields.
* The POST endpoint received `name`, `price`, and `stock` as parameters.
* Validation returned `200 OK` with error messages.
* Missing products returned `200 OK`.
* There was no service layer.
* Routes such as `/all` and `/get` were not RESTful.

### After

The API was refactored into separate layers with clearer responsibilities.

* `Product` is now a separate model with properties.
* `CreateProductRequest` is used for POST request data.
* `ProductResponse` is used for API responses.
* `IProductService` defines the service operations.
* `ProductService` contains storage, mapping, and product logic.
* `ProductsController` is responsible only for handling HTTP requests and responses.
* Invalid requests return `400 Bad Request`.
* Missing products return `404 Not Found`.
* Successful creation returns `201 Created`.
* Routes follow RESTful naming conventions.

## Project Structure

```text
task-06-api-standards-refactor-pack/
│
├── README.md
│
├── OriginalBadCode/
│   └── ProductsController.cs
│
└── RefactoredApi/
    │
    ├── Controllers/
    │   └── ProductsController.cs
    │
    ├── Models/
    │   └── Product.cs
    │
    ├── DTOs/
    │   ├── CreateProductRequest.cs
    │   └── ProductResponse.cs
    │
    ├── Services/
    │   ├── IProductService.cs
    │   └── ProductService.cs
    │
    └── Program.cs
```

## Validation

The `CreateProductRequest` DTO uses Data Annotations to validate incoming data.

Examples:

* Product name is required.
* Price cannot be negative.
* Stock cannot be negative.

## What I Learned

This refactoring showed me why separating responsibilities makes an API easier to maintain.
I learned how DTOs can control the data sent to and returned from an API.
I also learned how a service layer keeps business logic out of the controller.
Using RESTful routes makes API endpoints clearer and more consistent.
I learned that HTTP status codes should describe the actual result of an operation.
Data Annotations and `[ApiController]` can handle invalid request validation automatically.
Overall, refactoring bad code makes the project cleaner, easier to test, and easier to extend.
