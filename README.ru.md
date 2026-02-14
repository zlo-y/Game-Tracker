Game-Tracker 🎮

Game-Tracker — это высокопроизводительное backend-приложение на базе .NET, созданное для точного мониторинга игрового времени и фиксации этапов прохождения игр.

Проект построен с соблюдением принципов Clean Architecture и разделением ответственности через CQRS, что обеспечивает легкую масштабируемость и поддержку кода.
🏗 Архитектура и технологии

Проект реализован с использованием современных подходов к разработке на стеке .NET:

    Core: .NET 8 / ASP.NET Core

    Patterns: CQRS (Command Query Responsibility Segregation)

    Library: MediatR (включая Pipeline Behaviors для обработки сквозной логики)

    Validation: FluentValidation (интегрирована в пайплайн MediatR)

    ORM: Entity Framework Core

    Database: PostgreSQL (основная БД)

    Containerization: Docker & Docker Compose

    Auth: JWT-based Authentication (в процессе интеграции)

🛠 Реализованный функционал
Core API (CRUD)

Полностью реализованы операции (Create, Read, Update, Delete) для ключевых сущностей:

    Games: Управление списком игр в библиотеке пользователя.

    Game Time: Трекинг игровых сессий и фиксация «поинтов» (времени прохождения).

Infrastructure & Patterns

    Clean Architecture: Четкое разделение на слои (Domain, Application, Infrastructure, API).

    CQRS via MediatR: Команды на изменение данных и запросы на чтение разделены для чистоты кода.

    Pipeline Behaviors: Реализована автоматическая валидация входящих запросов перед выполнением хендлеров.

    Database: Настроена контейнеризация PostgreSQL через Docker для быстрого развертывания окружения.

🚀 Быстрый старт

    Клонирование и подготовка:
    Bash

    git clone https://github.com/your-username/game-tracker.git
    cd game-tracker

    Запуск инфраструктуры (PostgreSQL):
    Bash

    docker-compose up -d

    Обновление базы данных:
    Bash

    dotnet ef database update --project YourProject.Infrastructure --startup-project YourProject.API

    Запуск:
    Bash

    dotnet run --project YourProject.API

📈 В планах (Roadmap)

    [x] Реализация CRUD и базовой логики трекинга.

    [x] Внедрение CQRS и MediatR Pipeline.

    [ ] Auth Service: Пуш системы регистрации и авторизации в основной репозиторий.

    [ ] Redis: Внедрение кэширования для оптимизации нагрузки на PostgreSQL.

    [ ] UI: Разработка фронтенд-части.

⚙️ Статус разработки

Проект находится в стадии активного наполнения функционалом. В ближайшее время ожидается обновление с системой аутентификации.
