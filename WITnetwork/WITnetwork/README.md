<div align="center">

# WITnetwork Backend

🇬🇧 English | [🇺🇦 Українська](#українська)

</div>

---

# English

## About

WITnetwork Backend is the server-side part of the WITnetwork social networking platform. It provides REST APIs, real-time communication, authentication, file management, and business logic for the frontend application.

## Features

- 🔐 JWT Authentication & Authorization
- 👤 User Management
- 📝 Posts
- 💬 Real-time Chat (SignalR)
- 👥 Groups
- 🖼️ Photo Albums
- 🤝 Friends System
- 🔔 Notifications
- ☁️ Cloudinary Image Storage
- 📧 Email Verification
- 🗄️ PostgreSQL Database

## Tech Stack

### Backend

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- SignalR
- ASP.NET Identity
- AutoMapper
- FluentValidation
- JWT Authentication

### External Services

- Cloudinary
- Supabase (PostgreSQL)
- Brevo SMTP

## Project Structure

```text
Controllers/
Dtos/
Extensions/
Helpers/
Hubs/
Interfaces/
Mappings/
Middleware/
Models/
Repositories/
Services/
Settings/
```

## Installation

```bash
git clone <repository>

cd backend

dotnet restore
```

## Configuration

Copy

```text
appsettings.Development.json
```

or configure using environment variables.

Example:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": ""
    },

    "Jwt": {
        "Key": "",
        "Issuer": "",
        "Audience": ""
    },

    "CloudinarySettings": {
        "CloudName": "",
        "ApiKey": "",
        "ApiSecret": ""
    },

    "Brevo": {
        "ApiKey": ""
    }
}
```

## Database

Apply migrations

```bash
dotnet ef database update
```

Create new migration

```bash
dotnet ef migrations add MigrationName
```

## Run

```bash
dotnet run
```

or

```bash
dotnet watch
```

## API

REST API

```
https://localhost:5001/api
```

SignalR Hub

```
https://localhost:5001/chatHub
```

## Requirements

- .NET 9 SDK
- PostgreSQL

## Author

Nazar Zozulya

## License

MIT

---

# Українська

## Про проєкт

WITnetwork Backend — це серверна частина соціальної мережі WITnetwork. Проєкт забезпечує REST API, автентифікацію, роботу з базою даних, обробку бізнес-логіки, завантаження файлів та взаємодію з клієнтським застосунком у режимі реального часу.

## Можливості

- 🔐 JWT автентифікація та авторизація
- 👤 Керування користувачами
- 📝 Публікації
- 💬 Чат у реальному часі (SignalR)
- 👥 Групи
- 🖼️ Фотоальбоми
- 🤝 Система друзів
- 🔔 Сповіщення
- ☁️ Зберігання зображень у Cloudinary
- 📧 Підтвердження електронної пошти
- 🗄️ База даних PostgreSQL

## Стек технологій

### Backend

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- SignalR
- ASP.NET Identity
- AutoMapper
- FluentValidation
- JWT Authentication

### Зовнішні сервіси

- Cloudinary
- Supabase (PostgreSQL)
- Brevo SMTP

## Структура проєкту

```text
Controllers/
Data/
Dtos/
Helpers/
Hubs/
Mappings/
Models/
Services/
```

## Встановлення

```bash
git clone https://github.com/Nazar-Zozulya/WITNetwork.git

cd WITnetwork/WITnetwork

dotnet restore
```

## Налаштування

Заповніть файл

```text
appsettings.Development.json
```

або використайте змінні середовища.

## База даних

Створити нову міграцію

```bash
dotnet ef migrations add MigrationName
```

Застосувати міграції

```bash
dotnet ef database update
```


## Запуск

```bash
dotnet run
```

## API

### Swagger UI

```
https://localhost:5028/swagger
```

SignalR Hubs

```
https://localhost:5028/chat
```

```
https://localhost:5028/global
```

## Вимоги

- .NET 9 SDK
- PostgreSQL

## Автор

Назар Зозуля

## Ліцензія

MIT