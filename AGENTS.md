# AGENTS.md

ASP.NET Core Web API (.NET 10) + SQL Server 2022 for LOTR: LCG match logging. Data access is **Dapper with raw SQL** (no EF Core, no `DbContext`) — do not add EF migrations.

## Commands

```bash
dotnet build
dotnet run          # http://localhost:5130 (Dev), https://localhost:7052
dotnet watch run
```

No tests exist (no test project; `dotnet test` finds nothing — unit testing is on the roadmap). No lint/format config beyond .NET defaults.

## Architecture

`Controller -> Service (Services/Implementations + Interfaces) -> Repository (Repositories/Implementations + Interfaces) -> raw Dapper SQL` against `ConnectionStrings:DefaultConnection`.

- Every entity needs explicit DI registration in `Program.cs` (`AddScoped<IXService, XService>()` + `AddScoped<IXRepository, XRepository>()`, Program.cs:60-78). Easy to forget when adding a new entity.
- Route convention is `[Route("api/[controller]")]`. CRUD controllers wrap calls in try/catch returning `StatusCode(500, "Internal error: ...")`.
- Passwords: BCrypt hash/verify. JWT issued on login (`Jwt:*` in appsettings.json).

## Gotchas

- **`DateOnly` requires TWO registrations** or serialization breaks: `DateOnlyJsonConverter` (format `dd-MM-yyyy`) in `AddControllers().AddJsonOptions` (Program.cs:17-22) AND `SqlMapper.AddTypeHandler(new DateOnlyTypeHandler())` (Program.cs:88). Both must exist for `DateOnly` JSON + Dapper mapping.
- JSON is camelCase (`JsonNamingPolicy.CamelCase`).
- **`init.sql` does NOT create a `Users` table**, but `UserRepository`/`UserService`/`AuthenticationController` query one. Login/register will fail against the dockerized DB until the table is added to `init.sql`.
- **No Dockerfile in the repo**, yet `docker-compose.yml` has `build: .` for the `api` service — `docker-compose up -d` fails at the API build. Only the `db` service works.
- DB seeding: `init.sql` is mounted at `/docker-entrypoint-initdb.d/init.sql`; it drops & recreates tables. It uses `IF OBJECT_ID(...) DROP` ordering by FK dependencies — keep new tables consistent with that ordering.
- **PKs are manual IDs** (seeded from Excel, no IDENTITY) for Spheres/Cycles/Quests/Heroes/Results/Difficulties/ReasonsForDefeat; only `Games` uses IDENTITY(1,1). Don't assume auto-increment.
- `Game.Heroes` is a many-to-many via `GameHeroes` join table which carries `IsDead` — this maps onto `Hero.IsDead`; the entity is populated via Dapper multi-mapping with `splitOn: "Id"`.
- Swagger UI serves at `/` in Development (empty `RoutePrefix`); spec at `/swagger/v1/swagger.json`.
- `LOTR_GameRegister.Api.http` still hits `/weatherforecast` (stale — endpoint doesn't exist).
- Repos/servicers use primary-constructor injection (`class X(IConfiguration config) : IX`). Follow that style.
