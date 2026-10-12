# Secure Training Platform API
## Task 02 – Role Stories & Access Control
## Task 03 – Secure Platform Upgrade

## 1. Project Overview

The Secure Training Platform API is an ASP.NET Core Web API built with .NET 8, Entity Framework Core, SQL Server, ASP.NET Core Identity, and JWT Bearer Authentication.

The platform manages students, instructors, training tracks, enrollments, payments, and reports.

Tasks 02 and 03 focus on securing the API through role-based authorization, resource ownership validation, input validation, and protected access to private and administrative data.

## 2. Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / OpenAPI
- Postman

## 3. User Roles

The system supports three main roles.

### Admin
- Manage students and instructors.
- Create and manage training tracks.
- Assign instructors to training tracks.
- View and manage all enrollments.
- Update payment statuses.
- Access administrative and revenue reports.

### Instructor
- View assigned training tracks only.
- View students enrolled in their assigned tracks.
- Create sessions for their assigned tracks.
- Update notes for sessions they own through their assigned tracks.
- View progress reports for their assigned tracks.
- Cannot manage other instructors or access administrative revenue reports.

### Student
- View their own profile.
- Update permitted profile fields.
- View their own enrollments and payment history.
- View available training tracks.
- Request enrollment in a training track.
- Cannot access another student's private data or administrative reports.

## 4. Authentication and Authorization

The API uses ASP.NET Core Identity for account management and password hashing, and JWT Bearer tokens for authentication.

### Authentication Flow

1. The user registers or receives an account created by an administrator.
2. The user logs in with their email and password.
3. The API validates the credentials and account status.
4. A JWT access token is issued after successful authentication.
5. The client sends the token in the Authorization header when accessing protected endpoints.

Example:

`Authorization: Bearer <access_token>`

### Authorization Rules

- `[Authorize]` requires authentication.
- `[Authorize(Roles = "Admin")]` restricts access to administrators.
- `[Authorize(Roles = "Instructor")]` restricts access to instructors.
- `[Authorize(Roles = "Student")]` restricts access to students.
- `[Authorize(Roles = "Admin,Instructor")]` allows either role.
- `[AllowAnonymous]` is used only for endpoints intentionally available without authentication.

Authentication confirms the user's identity. Authorization checks whether that identity has permission to perform the requested operation.

## 5. Resource Ownership

Role checks alone are not sufficient for private resources. The API must also verify ownership.

### Student Ownership

The API obtains the authenticated user ID from the JWT claims and resolves the linked `StudentId` through the user's Identity account.

Students can access only their own:
- Profile
- Enrollments
- Payment history

Student IDs supplied by clients must not be trusted for private-data access.

### Instructor Ownership

The API resolves the current user's linked `InstructorId` and checks whether the requested training track belongs to that instructor.

Instructors can access only their assigned tracks, students enrolled in those tracks, sessions associated with those tracks, and corresponding progress reports.

An instructor must not be able to access or modify another instructor's resources by changing an ID in the request.

## 6. Instructor Account Management

Instructor accounts are created by administrators.

### Admin Responsibilities

- Create the instructor profile.
- Provide the instructor's initial email and password.
- Assign the Instructor role.
- Link the Identity account to the corresponding instructor record.
- Control account activation and administrative properties.

Passwords are stored using ASP.NET Core Identity password hashing and must never be stored as plain text.

### Instructor Responsibilities

After receiving an account, the instructor can log in and update permitted profile fields through:

`PUT /api/instructor/me`

The instructor cannot modify their own role, linked instructor ID, activation status, payment information, or other administrative properties.

## 7. API Endpoints and Access Matrix

The following table describes the intended API access rules for Tasks 02 and 03.

| Resource / Endpoint | Allowed Role | Access Rule |
|---|---|---|
| `POST /api/auth/register` | Anonymous | Register a permitted student account |
| `POST /api/auth/login` | Anonymous | Authenticate and obtain a JWT |
| `GET /api/auth/me` | Authenticated | Return the current user's identity information |
| `POST /api/auth/change-password` | Authenticated | Change the authenticated user's password |
| `GET /api/students` | Admin | List students with pagination and filters |
| `GET /api/students/{id}` | Admin | View a student record |
| Student CRUD endpoints | Admin | Manage student records |
| `GET /api/instructors` | Admin | List instructor records |
| Instructor CRUD endpoints | Admin | Create and manage instructor records |
| `PUT /api/instructor/me` | Instructor | Update permitted fields in their own profile |
| `GET /api/instructor/my-tracks` | Instructor | View assigned training tracks only |
| `GET /api/instructor/tracks/{id}/students` | Instructor | View students in an assigned track only |
| `POST /api/instructor/tracks/{id}/sessions` | Instructor | Create a session for an assigned track |
| `PUT /api/instructor/sessions/{id}` | Instructor | Update a session belonging to an assigned track |
| `GET /api/instructor/tracks/{id}/progress` | Instructor | View progress for an assigned track |
| `POST /api/tracks` | Admin | Create a training track |
| `PUT /api/tracks/{id}/assign-instructor` | Admin | Assign an active instructor to a track |
| Track management endpoints | Admin | Manage track records |
| `GET /api/tracks/available` | Public or Student | View available training tracks according to the configured policy |
| `GET /api/enrollments` | Admin | View all enrollments with supported filters |
| `PUT /api/enrollments/{id}/status` | Admin | Approve or update an enrollment status |
| `GET /api/student/my-enrollments` | Student | View the current student's enrollments |
| `POST /api/student/enrollment-requests` | Student | Request enrollment in a training track |
| `GET /api/student/my-payments` | Student | View the current student's payment history |
| `PUT /api/payments/{id}/status` | Admin | Update a payment status |
| `GET /api/reports/revenue-summary` | Admin | View revenue reports |
| `GET /api/reports/available-seats` | Admin | View available training track capacity |
| `GET /api/reports/top-tracks` | Admin | View top training tracks |
| `GET /api/reports/instructor-workload` | Admin | View instructor workload reports |

**Important:** These routes describe the intended API contract. Only endpoints implemented and tested in the final application should be marked as completed in the submission.

## 8. Business Rules and Validation

### Students
- Student email addresses must be unique.
- Deleted students must not appear in normal student listings.
- Student profile updates are restricted to permitted fields.
- Students cannot change their own role, account status, or administrative properties.
- Private student data must be protected against unauthorized access.

### Instructors
- Instructor email addresses must be unique.
- Instructor accounts are created by an administrator.
- Instructor accounts must be linked to an instructor profile.
- Instructors can access only their assigned training tracks.
- Instructors cannot create or manage other instructor accounts.
- Instructors cannot update sessions belonging to another instructor's track.

### Training Tracks
- A training track must have valid required fields.
- Track codes must be unique.
- Capacity must be greater than zero.
- The start date must be earlier than the end date.
- An assigned instructor must exist and be active.
- Deleted or unavailable tracks must not accept new enrollment requests.

### Enrollments
- An enrollment must reference an existing student and training track.
- The student identity must be resolved from the authenticated account.
- A student cannot have more than one pending or active enrollment in the same track.
- Enrollment status changes must follow the application's allowed status transitions.
- Enrollment requests must be rejected when the track is not accepting enrollments.

### Payments
- Payments must be associated with an existing enrollment.
- Students can view only payments associated with their own enrollments.
- Only authorized administrators can update payment statuses.
- Instructors must not access administrative revenue reports.
- Payment amounts and statuses must not be modifiable through student profile endpoints.

### Reports
- Revenue and administrative reports are restricted to administrators.
- Instructor progress reports must be scoped to the instructor's assigned tracks.
- Students must not access administrative reports.

## 9. HTTP Status Codes

| Status | Meaning |
|---|---|
| `200 OK` | The request succeeded |
| `201 Created` | A resource was created successfully |
| `400 Bad Request` | The request contains invalid data or violates request validation |
| `401 Unauthorized` | Authentication is missing, invalid, or expired |
| `403 Forbidden` | The authenticated user lacks the required role or resource permission |
| `404 Not Found` | The requested resource does not exist |
| `409 Conflict` | The request conflicts with an existing state or business rule |
| `500 Internal Server Error` | An unexpected server-side error occurred |

## 10. Security Testing with Postman

The following authorization tests are required for the Tasks 02 and 03 submission.

| # | Test | Expected Result |
|---|---|---|
| 1 | Request `GET /api/reports/revenue-summary` without a token | `401 Unauthorized` |
| 2 | Access the admin revenue report with a Student token | `403 Forbidden` |
| 3 | Instructor requests `GET /api/instructor/my-tracks` | `200 OK` |
| 4 | Instructor requests students in another instructor's track | `403 Forbidden` |
| 5 | Student requests `GET /api/student/my-enrollments` | `200 OK` |
| 6 | Student requests another student's private profile | `403 Forbidden` |
| 7 | Admin updates a payment status | `200 OK` |
| 8 | Student attempts to update a payment status | `403 Forbidden` |

Additional tests:
- Request enrollment twice in the same active track.
- Attempt to create a session for a track owned by another instructor.
- Attempt to update another instructor's session.
- Attempt to change administrative fields through a student profile update.
- Attempt to access protected endpoints with an expired or invalid token.
- Verify that a student sees only their own payments and enrollments.

Actual results should be recorded after running the tests against the implemented application.

---

**Project:** Secure Training Platform API  
**Framework:** ASP.NET Core Web API (.NET 8)  
**Database:** SQL Server  
**Authentication:** ASP.NET Core Identity + JWT Bearer  
**API Documentation:** Swagger / OpenAPI
