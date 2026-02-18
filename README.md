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
```
🛠 Features Implemented

    [x] Game Management: Full CRUD operations for the game library.

    [x] Time Tracking: CRUD for gaming sessions and milestone "time points".

    [x] CQRS Implementation: Complete separation of read and write operations.

    [x] Automatic Validation: No manual validation in controllers.
    
    [x] **Auth Service**: Registration & Login system (Identity + JWT).
    
    [x] **Infrastructure**: Full Dockerization (API + PostgreSQL).

    [ ] Redis Integration: Planned for high-speed caching.

🚀 Markdown

## 🚀 Getting Started

1. **Clone the repo:**
   ```bash
   git clone [https://github.com/your-username/game-tracker.git](https://github.com/your-username/game-tracker.git)

    Configure Environment:
    Create a .env file in the root directory (see .env.example).

    Run everything via Docker:
    Bash

    docker-compose up -d --build

    The API will be available at http://localhost:8080/swagger

   ```env
DB_USER=
DB_PASSWORD=
DB_NAME=
DB_PORT=5432
JWT_KEY=
JWT_ISSUER=
JWT_AUDIENCE=



---
*Note: This project is under active development. Expect breaking changes and frequent updates.*
