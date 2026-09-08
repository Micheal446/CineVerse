# 🎬 CineVerse - Web API (Enterprise MVP)

Welcome to **CineVerse**, a robust, secure, and production-ready RESTful Web API built with modern **ASP.NET Core**. This project serves as the reliable core backend engine designed to manage movie catalogs and genres, tailored specifically to handle clean data transfer and bulletproof security.

---

## 🚀 Key Features & Architectural Patterns

- **Full Control Data Validation (🛡️ Bulletproof Security)**: Implemented strict model constraints using Data Annotations (`[Range]`, `[Url]`, `[MinLength]`, `[MaxLength]`) to enforce absolute data integrity at the API gateway layer.
- **RESTful standard DTO Architecture**: Abstracted the underlying database layout using dedicated Data Transfer Objects (`CreateMovieDto`, `MovieResponseDto`, etc.) completely avoiding `Json Serialization Loops` (Infinite Loops) and ensuring light, clean communication with the client side.
- **ASP.NET Core Identity Integration**: Integrated built-in secure user account management for user registration and credential hashing.
- **Custom JWT Token Service (AuthService)**: Handled token generation out of controllers following the *Single Responsibility Principle (SRP)*. Features a smart **"Remember Me"** functionality dynamically adjusting expiration lifetimes up to 30 days.
- **Advanced LINQ Search & Filtering**: Developed high-performance, dynamic querying inside controllers to fetch records filtered by partial names or specific categories.
- **Automated Secure Search History**: Tracks the last 5 searched query terms per logged-in account, extracted implicitly from claims inside the incoming JWT header token.

---

## 🛠️ Tech Stack & Dependencies

* **Framework**: .NET Core 8.0 / 9.0 Web API
* **Database Engine**: Microsoft SQL Server
* **ORM / Database Mapping**: Entity Framework Core (Code-First Approach)
* **Security & Tokens**: Microsoft AspNetCore Identity & JWT Bearer
* **API Documentation**: Swagger UI (OpenAPI Specification)

---

## 📂 Project Structure Snapshot

- `Controllers/` — Handcrafted Controllers with full manual control over database logic.
- `models/` — Pure entity database mapping schemas.
- `DTOs/` — Lightweight transport structures optimized for performance.
- `services/` — Independent architectural service modules (`AuthService`).
- `Data/` — AppDbContext storage context configurations and Migrations.

---

## 💻 Setup and Run Local Server

1. **Clone the repository**:
   ```bash
   git clone https://github.com
   ```
2. **Update Connection String**: Modify `appsettings.json` connection properties to point to your local SQL Server instance.
3. **Apply Database Migrations**: Run the following sequence in the Package Manager Console:
   ```shell
   Add-Migration InitialCreate
   Update-Database
   ```
4. **Boot Up Application**: Execute the runtime profile via your IDE or terminal using `dotnet run`, then browse the interface map directly under:
   ```url
   https://localhost:7210/swagger
   ```
