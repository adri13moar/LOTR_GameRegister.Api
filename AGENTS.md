# AGENTS.md

ASP.NET Core Web API (.NET 10) + SQL Server 2022 for LOTR: LCG match logging. Data access is **Dapper with raw SQL** (no EF Core, no `DbContext`) — do not add EF migrations.

## Commands

```bash
dotnet build
dotnet run          # http://localhost:5130 (Dev), https://localhost:7052
dotnet watch run
```

Unit tests live in `LOTR_GameRegister.Tests` (MSTest + Moq + FluentAssertions) — run them with `dotnet test LOTR_GameRegister.Tests/LOTR_GameRegister.Tests.csproj`.

## Architecture

`Controller -> Service (LOTR_GameRegister.Application/Services) -> Repository (LOTR_GameRegister.Infrastructure/Repositories) -> raw Dapper SQL` against `ConnectionStrings:DefaultConnection`.

- Every entity needs explicit DI registration in `Program.cs` (`AddScoped<IXService, XService>()` + `AddScoped<IXRepository, XRepository>()`, Program.cs:60-78). Easy to forget when adding a new entity.
- Route convention is `[Route("api/[controller]")]`. Controllers return typed results with localized messages via `Helpers/Localizer.Get(...)` (`Resources/Messages.resx` + `Messages.es.resx`); unhandled exceptions go through `Helpers/ApiExceptionHandler`. Any changes to result shapes should keep localization intact.
- Passwords: BCrypt hash/verify. JWT issued on login (`Jwt:*` in appsettings.json).

## Gotchas

- **`DateOnly` requires TWO registrations** or serialization breaks: `DateOnlyJsonConverter` (format `dd-MM-yyyy`) in `AddControllers().AddJsonOptions` (Program.cs:17-22) AND `SqlMapper.AddTypeHandler(new DateOnlyTypeHandler())` (Program.cs:88). Both must exist for `DateOnly` JSON + Dapper mapping.
- JSON is camelCase (`JsonNamingPolicy.CamelCase`).
- **`init.sql` does NOT create a `Users` table**, but `UserRepository`/`UserService`/`AuthenticationController` query one. Login/register will fail against the dockerized DB until the table is added to `init.sql`.
- **`Dockerfile` exists at the repo root** and `docker-compose.yml` builds the `api` service with context `.` (repo root, so the solution is in scope); the API source lives in `LOTR_GameRegister.Api/`. The compose `db` service + `init.sql` work; `api` needs `MSSQL_SA_PASSWORD` + `JWT_KEY` (see `.env.example`).
- DB seeding: `init.sql` is mounted at `/docker-entrypoint-initdb.d/init.sql`; it drops & recreates tables. It uses `IF OBJECT_ID(...) DROP` ordering by FK dependencies — keep new tables consistent with that ordering.
- **PKs are manual IDs** (seeded from Excel, no IDENTITY) for Spheres/Cycles/Quests/Heroes/Results/Difficulties/ReasonsForDefeat; only `Games` uses IDENTITY(1,1). Don't assume auto-increment.
- `Game.Heroes` is a many-to-many via `GameHeroes` join table which carries `IsDead` — this maps onto `Hero.IsDead`; the entity is populated via Dapper multi-mapping with `splitOn: "Id"`.
- Swagger UI serves at `/` in Development (empty `RoutePrefix`); spec at `/swagger/v1/swagger.json`.
- `LOTR_GameRegister.Api/LOTR_GameRegister.Api.http` covers register → login (captures the JWT) → games CRUD → public lookups.
- Repos/servicers use primary-constructor injection (`class X(IConfiguration config) : IX`). Follow that style.
