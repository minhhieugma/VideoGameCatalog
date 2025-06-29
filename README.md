# 🎮 Video Game Catalog – Backend API

A clean, maintainable catalog API for video games built with **ASP.NET Core 8**, using:

- ✅ CQRS Architecture (Commands / Queries)
- ✅ DTOs for clean separation of concerns
- ✅ EF Core 8 (Code-First) with SQLite
- ✅ FluentValidation for input validation
- ✅ Unit Testing with xUnit
- ✅ Seeded data for easy testing
- ✅ REST client support via `.http` file

---

## 🔧 Tech Stack

| Layer         | Technology                        |
|---------------|-----------------------------------|
| API           | ASP.NET Core 8 Web API            |
| Data Access   | EF Core 8 + SQLite (Code First)   |
| Validation    | FluentValidation                  |
| Architecture  | CQRS (Command/Query separation)   |
| Testing       | xUnit                             |
| Tooling       | Rider / VS Code, `.http` client   |

---

## 🧱 Project Structure

```bash
VideoGameCatalog/
├── VideoGameCatalog.Api/         # ASP.NET Core API
│   ├── Controllers/              # REST endpoints (GET, POST, PUT, DELETE)
│   ├── Data/                     # EF DbContext + Seeder
│   ├── Models/                   # EF entities
│   ├── DTOs/                     # Create / Update / Output contracts
│   ├── Application/              # CQRS Commands and Queries
│   ├── Validation/               # FluentValidation validators
│   ├── Program.cs                # DI + Swagger + DB setup
│   └── VideoGameCatalog.API.http# REST client test script
├── VideoGameCatalog.Tests/       # xUnit test project
│   ├── Application/              # Command + Query unit tests
│   └── Validation/               # FluentValidation tests
├── global.json                   # .NET SDK lock
├── .gitignore                    # Git exclusions
└── README.md                     # You're reading it!
````

---

## ▶️ Running the API

### 📦 Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* SQLite ( - DB file/database is created automatically)

---

### 🚀 Start the API

```bash
cd VideoGameCatalog.Api
dotnet run
```

* Swagger UI available at: `http://localhost:5135/swagger`
* Database: `videogames.db` created + seeded with sample games

---

## ⚙️ EF Core Setup (Run once)

The SQLite database file (`videogames.db`) is **not committed to source control** — it is auto-created by EF Core when the app runs.

> If for any reason the file is not generated (e.g. seeding fails, startup crash), you can manually apply the schema using:

```bash
cd VideoGameCatalog.Api
dotnet ef database update
```

To install EF CLI (if not already installed):

```bash
dotnet tool install --global dotnet-ef
```

---

## 🌐 API Endpoints

All routes are under: `/api/videogames`

| Method | Endpoint               | Description                 |
| ------ | ---------------------- | --------------------------- |
| GET    | `/api/videogames`      | List all video games        |
| GET    | `/api/videogames/{id}` | Get game by ID              |
| POST   | `/api/videogames`      | Add a new game (Create DTO) |
| PUT    | `/api/videogames/{id}` | Update a game (Update DTO)  |
| DELETE | `/api/videogames/{id}` | Delete a game by ID         |

---

## 📬 API Test Samples (via `.http`)

```http
# Get All
GET http://localhost:5135/api/videogames

# Get By ID
GET http://localhost:5135/api/videogames/1

# Create
POST http://localhost:5135/api/videogames
Content-Type: application/json

{
  "title": "Test Game",
  "genre": "RPG",
  "releaseDate": "2022-01-01"
}

# Update
PUT http://localhost:5135/api/videogames/1
Content-Type: application/json

{
  "id": 1,
  "title": "Updated Game",
  "genre": "Adventure",
  "releaseDate": "2023-03-01"
}

# Delete
DELETE http://localhost:5135/api/videogames/1
```

> Testable directly via VS Code or JetBrains Rider.

---

## ✅ Unit Testing

```bash
cd VideoGameCatalog.Tests
dotnet test
```

* ✅ AddVideoGameCommandTests
* ✅ UpdateVideoGameCommandTests
* ✅ GetAllVideoGamesQueryTests
* ✅ GetVideoGameByIdQueryTests
* ✅ Create/Update DTO validator tests

---

## 💡 Highlights

* CQRS-style clean separation of read/write logic
* DTOs used for all external communication
* FluentValidation for Create/Update rules
* Commands and Queries are unit-tested
* Self-contained with seeded data
