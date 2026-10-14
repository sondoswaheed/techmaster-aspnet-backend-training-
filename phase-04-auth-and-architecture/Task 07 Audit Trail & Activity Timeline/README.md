# Task 07 – Audit Trail & Activity Timeline

## Overview
This task implements an Audit Trail and Activity Timeline for the Training Center API. It records important user activities and system operations to provide traceability, accountability, and easier auditing.

## Objectives
- Track important actions performed by users.
- Store activity details in the database.
- Identify the user and role responsible for an action.
- Record the affected entity and its ID.
- Support filtering and pagination when retrieving activity logs.
- Restrict access to activity logs to Admin users.

## ActivityLog Entity
The `ActivityLog` entity stores information about each recorded activity.

| Property | Description |
|---|---|
| `Id` | Unique activity log identifier |
| `UserId` | ID of the user who performed the action |
| `UserRole` | Role of the user |
| `Action` | Type of action performed |
| `EntityName` | Name of the affected entity |
| `EntityId` | ID of the affected record |
| `Description` | Human-readable activity description |
| `CreatedAt` | Date and time of the activity |
| `IpAddress` | IP address associated with the request, when available |
| `Metadata` | Optional additional activity details stored as JSON |
| `RequestId` | Request identifier for troubleshooting and traceability |

## Activities to Be Logged
The system records the following important activities:

- User registration
- User login
- Training track creation
- Enrollment request
- Enrollment status update
- Payment creation
- Payment status update

Additional activities, such as report viewing and role changes, can be logged as optional enhancements.

## API Endpoint

### Get Activity Logs

**Endpoint:** `GET /api/admin/activity-logs`

**Authorization:** Admin role required.

**Query Parameters:**

| Parameter | Description |
|---|---|
| `pageNumber` | Page number |
| `pageSize` | Number of records per page |
| `userId` | Filter logs by user ID |
| `entityName` | Filter logs by entity name |
| `from` | Filter logs from a specified date |
| `to` | Filter logs up to a specified date |

The endpoint returns paginated activity logs and supports filtering by user, entity, and date range.

## Pagination
The paginated response includes:

- `Items`: Activity log records for the current page.
- `PageNumber`: Current page number.
- `PageSize`: Number of records per page.
- `TotalCount`: Total number of matching records.
- `TotalPages`: Total number of pages.

Activity logs are ordered by creation date, with the newest records returned first.

## Security
- The activity logs endpoint is restricted to users with the Admin role.
- Authentication is handled through JWT Bearer tokens.
- User identity and role information can be obtained from the authenticated user's claims.
- Sensitive information, such as passwords and access tokens, should never be stored in activity logs.

## Error Handling
The API uses the project's standardized `ApiResponse<T>` response structure for successful responses and handled errors.

Global exception handling and request validation are managed by the existing API middleware and validation configuration.


Run the migration only if it has not already been created and applied.

## Technologies Used
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication and Role-Based Authorization
- Dependency Injection
- LINQ
- ASP.NET Core Logging

## Expected Outcome
The Training Center API provides an audit trail for important user activities, allowing authorized administrators to review activity history, identify responsible users, and filter records efficiently.

This improves accountability, traceability, and maintainability across the system.
