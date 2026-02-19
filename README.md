# Udemy Second Project – Build ASP.NET Core Web API (Scratch To Finish) — .NET 8

🌐 **Live Demo:** *(add your live link here)*  
🎓 **Udemy Course:** https://www.udemy.com/course/build-rest-apis-with-aspnet-core-web-api-entity-framework/  
📜 **Certificate:** https://drive.google.com/file/d/1NwiUUTDsaXxo0j1Y90J-ymNUaBb7sdz0/view?usp=drive_link  
🎥 **Testing / Walkthrough Video:** https://drive.google.com/file/d/1VBeO3Ic19n0EZz4zRbXK75kIDVyQnsv9/view?usp=drive_link  

---

## Overview

A production-ready **ASP.NET Core Web API** built with **.NET 8**, following **Onion Architecture** principles.  
This project focuses on clean separation of concerns, authentication & authorization, advanced querying, and scalable backend design.

Built as part of the Udemy course: **Build ASP.NET Core Web API – Scratch To Finish**.

---

## Key Features

- ✅ Onion Architecture (**7 Layers**)
- ✅ ASP.NET Core Web API (**.NET 8**)
- ✅ JWT Authentication & Authorization
- ✅ Token Revocation (Logout)
- ✅ Role-based access (**Reader / Writer**)
- ✅ Unit of Work & Repository Pattern (**non-generic**)
- ✅ Advanced **Filtering / Sorting / Pagination** using **Expression Trees**
- ✅ API Versioning (**V1 / V2**)
- ✅ File Upload & Download
- ✅ Global Middlewares & Custom Filters
- ✅ Logging (Console & Files)
- ✅ Swagger with Authentication & Versioning
- ✅ MVC Client for API consumption & testing

---

## Architecture (Onion Architecture – 7 Layers)

**API → Application → Domain → Data → Identity → IoC → UI (MVC)**  
Each layer has a clear responsibility and depends only on inner layers.

---

## Layer Details

### Domain Layer
- Contains core **domain entities (models)**.
- Defines repository interfaces for:
  - Data layer
  - Identity layer
- Includes a **Unit of Work interface** that aggregates all repositories.
- Contains **pure abstractions only** (no EF Core or framework dependencies).
- Uses **GUID** as the primary key for all entities.

### Data Layer
- Contains the main **Application DbContext**.
- Includes:
  - EF Core migrations
  - Repository implementations
  - Unit of Work implementations
- Seeds initial data using `HasData()`.
- `GetAll()` returns `IQueryable` to allow:
  - Filtering
  - Sorting
  - Pagination  
  to be composed later in the service layer.

### Identity Layer
- Uses a separate **Identity DbContext**.
- Includes:
  - Identity domain models
  - Repository implementations
  - Identity data seeding
- Handles authentication & authorization logic.
- Roles included:
  - **Reader**
  - **Writer**
- JWT tokens are stored in the Identity database.
- Includes an Auth Repository that wraps:
  - `UserManager`
  - `RoleManager`
  - Authentication & authorization operations

### Application Layer
- Contains request/response **DTOs** (success & failure).
- Implements **service interfaces and implementations**.
- Includes **AutoMapper** profiles.
- Handles:
  - Business logic
  - Validation
  - Query composition
- Uses `IHttpContextAccessor` to access HTTP request data inside services.

### IoC Layer
- Centralized **Dependency Injection** container.
- Registers:
  - DbContexts
  - Repositories
  - Unit of Work
  - Services
  - JWT token services
  - AutoMapper
- Keeps startup configuration clean and maintainable.

### API Layer
- Thin RESTful controllers (no business logic).
- Implements:
  - Custom Filter Attributes
  - Global Middlewares for:
    - Exception handling
    - Token validation (existence & revocation via **JTI**)
    - Logging
- Supports API Versioning:
  - **V1:** single filter & single sort
  - **V2:** multiple filters & multiple sorts
- Swagger configured with:
  - JWT Authentication
  - API Versioning
- Static files enabled with custom `wwwroot` configuration.

### UI (MVC) Layer
- MVC client application used to consume and test the API.
- Uses MVC Controllers + Razor Views.
- Focused on testing the **Region** entity to validate:
  - Authentication / Authorization
  - Filtering, sorting & pagination
  - File upload & download

---

## Authentication & Authorization

- JWT tokens are created using:
  - User claims
  - User roles
- Each token includes a unique **JTI (GUID)**.
- Tokens are persisted in the database.
- Logout is implemented by revoking tokens using JTI.
- Token validation middleware checks:
  - Token existence
  - Token revocation status

---

## Advanced Querying (Filtering, Sorting & Pagination)

Implemented using **Expression Trees**.

### GetAll supports:
- `FilterParams[]`
- `SortParams[]`
- Pagination parameters

### Filtering:
- Dynamically builds expressions based on entity properties
- Supports string filtering using `ToLower()` & `Contains()`
- Handles null and empty checks

### Sorting:
- Dynamic sorting by property name
- Supports **ASC / DESC**

Queries are composed before materialization (`ToList()`).

---

## File Upload & Download

- Supports image upload and download.
- Images are:
  - Stored locally
  - Saved with their URL paths in the database
- Download:
  - Reads file as byte array
  - Returns it with the stored `Content-Type`

---

## Testing

Full end-to-end testing is demonstrated in the video, covering:
- Auth flow
- CRUD operations
- Advanced querying
- Token revocation
- File handling
