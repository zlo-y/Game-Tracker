# Game-Tracker 🎮

[Читать на русском](README.ru.md)

**Game-Tracker** is a professional-grade backend service built with .NET 8, designed to help gamers track their time spent in-game and mark specific milestones (time points) for completion analysis.

The project is a demonstration of **Clean Architecture** and modern design patterns, ensuring the code is decoupled, testable, and scalable.



---

## 🏗 Architecture & Technologies

* **Core Framework:** .NET 8 / ASP.NET Core
* **Architectural Pattern:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Design Pattern:** CQRS via **MediatR**
* **Validation:** **FluentValidation** integrated into the MediatR Pipeline
* **Database:** **PostgreSQL** with Entity Framework Core
* **Infrastructure:** **Docker & Docker Compose** for seamless deployment
* **Security:** Global Exception Handling Middleware & JWT Auth (In Progress)

---

## 🛡 Request Pipeline & Middleware

The application uses a custom request pipeline to ensure reliability:

1.  **Global Exception Middleware:** Intercepts all application errors. It specifically handles `ValidationException`, returning a structured JSON response to the client.
2.  **MediatR Pipeline Behaviors:** Automatically validates every Command/Query before it reaches the logic handler.



### Structured Error Response Example:
```json
{
  "statusCode": 400,
  "title": "Validation Error",
  "message": "One or more parameters failed the check.",
  "errors": [
    {
      "field": "GameTitle",
      "error": "'GameTitle' must not be empty."
    }
  ]
}

🛠 Features Implemented

    [x] Game Management: Full CRUD operations for the game library.

    [x] Time Tracking: CRUD for gaming sessions and milestone "time points".

    [x] CQRS Implementation: Complete separation of read and write operations.

    [x] Automatic Validation: No manual validation in controllers.

    [ ] Auth Service: Registration & Login system (JWT) - Coming soon.

    [ ] Redis Integration: Planned for high-speed caching.

🚀 Getting Started

    Clone the repo:
    Bash

    git clone [https://github.com/your-username/game-tracker.git](https://github.com/your-username/game-tracker.git)

    Start PostgreSQL via Docker:
    Bash

    docker-compose up -d

    Update Database:
    Bash

    dotnet ef database update

    Run:
    Bash

    dotnet run --project WebAPI

---
*Note: This project is under active development. Expect breaking changes and frequent updates.*
