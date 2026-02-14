# Game-Tracker 🎮

[Read in Russian | Читать на русском](README.ru.md)

---

**Game-Tracker** is a high-performance .NET-based backend application designed for precise gaming time monitoring and milestone tracking. 

Built with **Clean Architecture** principles and **CQRS**, the project ensures high maintainability, scalability, and clear separation of concerns.

## 🏗 Architecture & Technologies

The project follows modern software engineering patterns within the .NET ecosystem:

* **Core:** .NET 8 / ASP.NET Core
* **Design Patterns:** CQRS (Command Query Responsibility Segregation)
* **Messaging:** MediatR (including Pipeline Behaviors for cross-cutting concerns)
* **Validation:** FluentValidation (integrated into the MediatR pipeline)
* **ORM:** Entity Framework Core
* **Database:** PostgreSQL (Primary storage)
* **Containerization:** Docker & Docker Compose
* **Auth:** JWT-based Authentication (In progress)

## 🛠 Features Implemented

### **Core API (CRUD)**
Fully functional RESTful endpoints for:
* **Games:** Comprehensive management of the user's game library.
* **Game Time:** Precise tracking of gaming sessions and "Time Points" (milestones for completion).

### **Infrastructure & Patterns**
* **Clean Architecture:** Strict separation into Domain, Application, Infrastructure, and API layers.
* **CQRS via MediatR:** Decoupled commands and queries for cleaner, more testable code.
* **Pipeline Behaviors:** Automated request validation and logging handled globally before reaching the handlers.
* **Containerized DB:** Pre-configured PostgreSQL environment using Docker for "one-click" setup.

## 🚀 Quick Start

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/game-tracker.git](https://github.com/your-username/game-tracker.git)
    cd game-tracker
    ```

2.  **Spin up the infrastructure:**
    ```bash
    docker-compose up -d
    ```

3.  **Apply migrations:**
    ```bash
    dotnet ef database update --project YourProject.Infrastructure --startup-project YourProject.API
    ```

4.  **Run the application:**
    ```bash
    dotnet run --project YourProject.API
    ```

## 📈 Roadmap
- [x] CRUD operations & core tracking logic.
- [x] CQRS & MediatR Pipeline integration.
- [ ] **Auth Service:** Push Registration & Login system to the main branch.
- [ ] **Redis:** Implement caching to reduce PostgreSQL load.
- [ ] **Frontend:** Develop a dashboard for data visualization.

---
*Note: This project is under active development. Expect breaking changes and frequent updates.*
