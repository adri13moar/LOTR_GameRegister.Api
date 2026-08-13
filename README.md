# 🧙‍♂️ LOTR Game Register API

> A robust backend solution for The Lord of the Rings: LCG players
>
> *Languages / Idiomas: [English](#english-version) | [Español](#versión-en-español)*

---

## English Version

### Overview

**LOTR Game Register** is a robust backend solution for *The Lord of the Rings: LCG* players. It transforms flat Excel data into a normalized relational database for advanced match logging and statistical analysis.

### 🚀 Key Features

- **Normalized Architecture** — 12-table relational schema designed to eliminate data redundancy
- **Automatic Seeding** — Custom `init.sql` script that auto-populates 103 heroes and 121 quests upon deployment
- **Dockerized Environment** — Instant setup via Docker Compose for both SQL Server and the API
- **Advanced Analytics** — Ready for complex queries (e.g., win rates per hero or sphere)
- **Swagger Documentation** — Interactive API documentation out of the box
- **JWT Authentication** — Secure login/register with role-based authorization (Admin) and login rate limiting
- **Localization** — User-facing messages served in English or Spanish via the `Accept-Language` header
- **Unit Tests** — MSTest suite covering the service layer (54 tests)

### 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| **Backend** | ASP.NET Core Web API (.NET 10) |
| **Database** | Microsoft SQL Server 2022 |
| **Data Access** | Dapper (raw SQL, no EF Core) |
| **Auth** | JWT (Bearer) + BCrypt password hashing |
| **Infrastructure** | Docker & Docker Compose |
| **Testing** | MSTest + Moq + FluentAssertions |

### 📦 Quick Start

#### Option A — Docker (SQL Server + API)

Spin up the database (schema + seed via `init.sql`) and the API together:

```bash
# 1. Create the secrets file (once)
cp .env.example .env          # then edit the values

# 2. Start SQL Server + API (initializes schema + seed data)
docker compose up -d

# 3. Configure the connection string + JWT key (local development)
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=LOTR_GameRegister;User Id=sa;Password=<YOUR_SA_PASSWORD>;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "<A_RANDOM_SECRET_AT_LEAST_32_CHARS>"

# 4. Run the API
dotnet run
```

> The database container runs `init.sql` automatically on first start (it drops and
> recreates all tables). To re-seed, remove the volume and start again.

#### Option B — Without Docker

You need a running SQL Server 2022 instance. Run `init.sql` against it, then follow
steps 3–4 above.

#### Access the API

- Swagger UI: http://localhost:5130 (Development)
- HTTPS: https://localhost:7052
- API Base URL: http://localhost:5130

#### Authentication

1. `POST /api/authentication/register` with `{ "username", "email", "password" }`.
2. `POST /api/authentication/login` with `{ "username", "password" }` → returns a JWT.
3. Call protected endpoints (e.g. `/api/games`) with header `Authorization: Bearer <token>`.

#### Local environment setup (secrets)

The API requires a `Jwt:Key` and a DB connection string; never commit them. Configure
them once per machine (stored in `~/.microsoft/usersecrets` or as env vars):

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "<A_RANDOM_SECRET_AT_LEAST_32_CHARS>"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=LOTR_GameRegister;User Id=sa;Password=<YOUR_SA_PASSWORD>;TrustServerCertificate=True;"
```

When running via Docker Compose, fill `.env` instead (see `.env.example`) — the compose
`api` service maps `MSSQL_SA_PASSWORD` and `JWT_KEY` to the connection string and JWT key.

Testing without a hand-installed SQL Server: `docker compose up -d db` seeds the full
database (121 quests, 103 heroes, 10 example games). The unit test suite is fully mocked
and runs with no database: `dotnet test LOTR_GameRegister.Tests/LOTR_GameRegister.Tests.csproj`.

### 📊 Data Insights (SQL Example)

Get match statistics per hero:

```sql
SELECT 
    h.Id,
    h.Name, 
    COUNT(gh.GameId) as MatchesPlayed
FROM Heroes h
LEFT JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Id, h.Name
ORDER BY MatchesPlayed DESC;
```

### 🛣️ Roadmap

- [x] Requirement documentation and DB Schema design
- [x] SQL initialization script (`init.sql`)
- [x] Docker Compose configuration
- [x] Unit Testing implementation
- [ ] Advanced Statistics Dashboard endpoint
- [ ] Command Handler pattern + Semantic Kernel integration

---

## Versión en Español

### Descripción General

**LOTR Game Register** es una solución backend robusta diseñada para coleccionistas y jugadores del juego de cartas *The Lord of the Rings: LCG*. Permite registrar partidas, gestionar una base de datos normalizada y realizar análisis estadísticos avanzados.

### 🚀 Características Principales

- **Base de Datos Normalizada** — Arquitectura relacional completa con 12 tablas, eliminando redundancias del Excel original
- **Seeding Automático** — Incluye un script `init.sql` que puebla el sistema con datos reales de la comunidad automáticamente
- **Contenedorización** — Despliegue inmediato mediante Docker, garantizando un entorno consistente
- **Relaciones Complejas** — Implementación de relaciones Many-to-Many entre Partidas y Héroes
- **Documentación Swagger** — Documentación interactiva de la API lista para usar
- **Autenticación JWT** — Login/registro seguro con autorización por roles (Admin) y limitación de intentos de login
- **Localización** — Mensajes de usuario en inglés o español según la cabecera `Accept-Language`
- **Tests Unitarios** — Suite MSTest para la capa de servicios (54 tests)

### 🛠️ Stack Tecnológico

| Componente | Tecnología |
|-----------|-----------|
| **Backend** | ASP.NET Core Web API (.NET 10) |
| **Base de Datos** | Microsoft SQL Server 2022 |
| **Acceso a Datos** | Dapper (SQL directo, sin EF Core) |
| **Autenticación** | JWT (Bearer) + hash de contraseñas BCrypt |
| **Infraestructura** | Docker & Docker Compose |
| **Testing** | MSTest + Moq + FluentAssertions |

### 📦 Inicio Rápido

#### Opción A — Docker (SQL Server + API)

Levanta la base de datos (esquema + seed mediante `init.sql`) y la API juntas:

```bash
# 1. Crea el archivo de secretos (una vez)
cp .env.example .env          # y edita los valores

# 2. Inicia SQL Server + API (inicializa esquema + datos de seed)
docker compose up -d

# 3. Configura la cadena de conexión y la clave JWT (desarrollo local)
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=LOTR_GameRegister;User Id=sa;Password=<TU_PASSWORD_SA>;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "<UN_SECRETO_ALEATORIO_DE_AL_MENOS_32_CARACTERES>"

# 4. Ejecuta la API
dotnet run
```

> El contenedor de la base de datos ejecuta `init.sql` automáticamente en el primer
> arranque (borra y recrea todas las tablas). Para volver a sembrar, elimina el
> volumen y vuelve a iniciar.

#### Opción B — Sin Docker

Necesitas una instancia de SQL Server 2022 en ejecución. Ejecuta `init.sql` contra ella
y sigue los pasos 3–4 anteriores.

#### Acceso a la API

- Swagger UI: http://localhost:5130 (Development)
- HTTPS: https://localhost:7052
- URL Base de la API: http://localhost:5130

#### Autenticación

1. `POST /api/authentication/register` con `{ "username", "email", "password" }`.
2. `POST /api/authentication/login` con `{ "username", "password" }` → devuelve un JWT.
3. Llama a los endpoints protegidos (p. ej. `/api/games`) con la cabecera `Authorization: Bearer <token>`.

#### Configuración local (secretos)

La API necesita una `Jwt:Key` y una cadena de conexión a la BD; nunca las compartas. Configúralas
una vez por máquina (se guardan en `~/.microsoft/usersecrets` o como variables de entorno):

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "<UN_SECRETO_ALEATORIO_DE_AL_MENOS_32_CARACTERES>"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=LOTR_GameRegister;User Id=sa;Password=<TU_PASSWORD_SA>;TrustServerCertificate=True;"
```

Cuando uses Docker Compose, rellena `.env` en su lugar (ver `.env.example`) — el servicio
`api` del compose mapea `MSSQL_SA_PASSWORD` y `JWT_KEY` a la cadena de conexión y la clave JWT.

Para probar sin tener SQL Server instalado a mano: `docker compose up -d db` siembra la base
completa (121 aventuras, 103 héroes, 10 partidas de ejemplo). La suite de tests unitarios está
totalmente mockeada y no necesita base de datos: `dotnet test LOTR_GameRegister.Tests/LOTR_GameRegister.Tests.csproj`.

### 📊 Ejemplo de Consulta (SQL)

Obtén estadísticas de partidas por héroe:

```sql
SELECT 
    h.Id,
    h.Name, 
    COUNT(gh.GameId) as PartidasJugadas
FROM Heroes h
LEFT JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Id, h.Name
ORDER BY PartidasJugadas DESC;
```

### 🛣️ Roadmap

- [x] Documentación de requisitos y diseño de BD
- [x] Script de inicialización SQL (`init.sql`)
- [x] Configuración de Docker Compose
- [x] Implementación de Unit Testing
- [ ] Dashboard de estadísticas avanzado
- [ ] Patrón Command Handler + integración con Semantic Kernel

---

## 👨‍💼 Author / Autor

**Adrián Molina Arroyo**

- 🔗 [LinkedIn](https://www.linkedin.com/in/molinaarroyoadrian)
- 📁 [GitHub Portfolio](https://github.com/adri13moar)

---

*Made with ❤️ for The Lord of the Rings: LCG community*