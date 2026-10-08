# Task 01 - Authentication Foundation

## Overview

The authentication system provides secure user registration, login, JWT authentication, role-based authorization, and current-user information.

The system uses ASP.NET Core Identity for user management and password hashing, and JWT Bearer Authentication for securing API endpoints.

---

## User Roles

The system supports three roles:

* Admin
* Instructor
* Student

Public registration is allowed for Students only.

Admin and Instructor accounts should be created or controlled by an authorized Admin.

---

## User Entity

`ApplicationUser` inherits from `IdentityUser`.

Identity provides built-in properties such as:

* Id
* Email
* PasswordHash
* UserName

The application adds:

* FullName
* Role
* IsActive
* CreatedAt
* UpdatedAt
* LastLoginAt
* StudentId
* InstructorId

Passwords are never stored as plain text.

ASP.NET Core Identity hashes the password before storing it in the database.

---

## Register Flow

Endpoint:

`POST /api/auth/register`

The registration flow is:

1. Receive RegisterRequest.
2. Validate the request.
3. Allow Student registration only.
4. Check email uniqueness.
5. Create the Student record.
6. Create the ApplicationUser.
7. Identity hashes the password.
8. Assign the Student role.
9. Return a safe registration response.

Register does not return a JWT token.

Example response:

```json
{
  "userId": "user-id",
  "fullName": "Mohamed Ayman",
  "email": "mohamed@example.com",
  "role": "Student"
}
```

---

## Login Flow

Endpoint:

`POST /api/auth/login`

The login flow is:

1. Receive LoginRequest.
2. Find the user by email.
3. Check whether the account is active.
4. Verify the password using ASP.NET Core Identity.
5. Update LastLoginAt.
6. Generate a JWT access token.
7. Return AuthResponse.

Example response:

```json
{
  "accessToken": "JWT_TOKEN",
  "expiresAt": "2026-10-08T12:00:00Z",
  "userId": "user-id",
  "fullName": "Mohamed Ayman",
  "email": "mohamed@example.com",
  "role": "Student"
}
```

---

## JWT Flow

After successful login, the server generates a signed JWT access token.

The client sends the token with protected requests:

`Authorization: Bearer {token}`

The API validates:

* Token signature
* Issuer
* Audience
* Expiration
* JWT structure

Invalid or expired tokens are rejected.

---

## JWT Claims

The token contains the following application claims:

### NameIdentifier

Represents the user ID.

```csharp
new Claim(ClaimTypes.NameIdentifier, user.Id)
```

It can be used to identify the authenticated user.

### Email

Represents the user's email.

```csharp
new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
```

### Role

Represents the user's authorization role.

```csharp
new Claim(ClaimTypes.Role, role)
```

It is used by `[Authorize(Roles = "...")]`.

### Name

Represents the user's full name.

```csharp
new Claim(ClaimTypes.Name, user.FullName)
```

### Expiration

The JWT also contains an expiration value (`exp`).

The current implementation creates tokens that expire after two hours.

---

## Protected Endpoints

Endpoints that require authentication use:

```csharp
[Authorize]
```

Role-protected endpoints can use:

```csharp
[Authorize(Roles = "Admin")]
```

or:

```csharp
[Authorize(Roles = "Instructor")]
```

or:

```csharp
[Authorize(Roles = "Student")]
```

---

## Current User

Endpoint:

`GET /api/auth/me`

This endpoint requires a valid JWT.

It reads the authenticated user ID from:

```csharp
User.FindFirstValue(ClaimTypes.NameIdentifier)
```

The response contains:

* UserId
* FullName
* Email
* Role
* LinkedStudentId
* LinkedInstructorId

PasswordHash and other sensitive information are never returned.

---

## Change Password

Endpoint:

`POST /api/auth/change-password`

This endpoint requires authentication.

The request contains:

* Email
* CurrentPassword
* NewPassword
* ConfirmNewPassword

The current password is verified before the new password is saved.

ASP.NET Core Identity handles password hashing.

---

## Security Rules

The system enforces:

* Unique email addresses
* Strong password requirements
* Password hashing
* Active-user validation
* JWT expiration
* JWT signature validation
* Role-based authorization
* Safe authentication responses

---

## Required Endpoints

| Method | Endpoint                    | Authentication |
| ------ | --------------------------- | -------------- |
| POST   | `/api/auth/register`        | Anonymous      |
| POST   | `/api/auth/login`           | Anonymous      |
| GET    | `/api/auth/me`              | Required       |
| POST   | `/api/auth/change-password` | Required       |



10. Expired/invalid token test where applicable.
