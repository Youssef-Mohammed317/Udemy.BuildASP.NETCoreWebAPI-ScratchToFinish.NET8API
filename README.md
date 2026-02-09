Build ASP.NET Core Web API – Scratch To Finish (.NET 8)

A production-ready ASP.NET Core Web API built with .NET 8, following Onion Architecture principles.
This project focuses on clean separation of concerns, authentication & authorization, advanced querying, and scalable backend design.

Built as part of the Udemy course:
Build ASP.NET Core Web API – Scratch To Finish.

Key Features

Onion Architecture (7 Layers)

ASP.NET Core Web API (.NET 8)

JWT Authentication & Authorization

Token Revocation (Logout)

Roles-based access (Reader / Writer)

Unit of Work & Repository Pattern (non-generic)

Advanced Filtering, Sorting & Pagination using Expression Trees

API Versioning (V1 / V2)

File Upload & Download

Global Middlewares & Custom Filters

Logging (Console & Files)

Swagger with Authentication & Versioning

MVC Client for API consumption & testing

Architecture Overview

The solution follows Onion Architecture with 7 layers:

API – Application – Domain – Data – Identity – IoC – UI (MVC)

Each layer has a clear responsibility and depends only on inner layers.

Domain Layer

Contains domain entities (models).

Defines repository interfaces for:

Data layer

Identity layer

Includes a Unit of Work interface that abstracts all repositories.

Contains only pure abstractions (no EF Core or framework dependencies).

Uses GUID as the primary key for all entities.

Data Layer

Contains the main Application DbContext.

Includes:

EF Core migrations

Repository implementations

Unit of Work implementations

Seeds initial data using HasData().

Repositories return IQueryable for GetAll() to allow:

Filtering

Sorting

Pagination
to be applied later in the service layer.

Identity Layer

Contains a separate Identity DbContext.

Includes:

Identity domain models

Repository implementations

Identity data seeding

Handles authentication & authorization logic.

Two roles are defined:

Reader

Writer

JWT tokens are stored in the Identity database.

Includes an Auth Repository that wraps:

UserManager

RoleManager

Authentication & authorization operations

Application Layer

Contains DTOs for requests and responses (success & failure).

Implements service interfaces and implementations.

Includes AutoMapper profiles.

Handles:

Business logic

Validation

Query composition

Uses IHttpContextAccessor to access HTTP request data inside services.

IoC Layer

Centralized Dependency Injection container.

Registers:

DbContexts

Repositories

Unit of Work

Services

JWT token services

AutoMapper

Keeps application startup clean and maintainable.

API Layer

Contains thin RESTful controllers.

Implements:

Custom Filter Attributes

Global Middlewares for:

Exception handling

Token validation (existence & revocation via JTI)

Logging

Supports API Versioning:

V1: single filter & single sort

V2: multiple filters & multiple sorts

Swagger configured with:

JWT Authentication

API Versioning

Static files enabled with custom wwwroot configuration.

Authentication & Authorization

JWT tokens are created using:

User claims

User roles

Each token contains a unique JTI (GUID).

Tokens are persisted in the database.

Logout is implemented by revoking tokens using JTI.

Token validation middleware checks:

Token existence

Token revocation status

Advanced Querying (Filtering, Sorting & Pagination)

Implemented using Expression Trees.

GetAll request supports:

FilterParams[]

SortParams[]

Pagination parameters

Filtering:

Dynamically builds expressions based on entity properties

Supports string filtering using ToLower() & Contains()

Handles null and empty checks

Sorting:

Dynamic sorting by property name

Supports ASC / DESC

Queries are composed before materialization (ToList()).

File Upload & Download

Supports image upload and download.

Images are:

Stored locally

Saved with their URL paths in the database

Download:

Reads file as byte array

Returns it with the stored Content-Type.

UI (MVC) Layer

Acts as a client application to consume and test the API.

Uses MVC controllers and Razor views.

Focuses on testing the Region entity.

Used to validate:

Authentication

Authorization

Filtering, sorting & pagination

File upload & download

Testing

Full project testing is demonstrated through a recorded video.

Covers:

Auth flow

CRUD operations

Advanced querying

Token revocation

File handling
