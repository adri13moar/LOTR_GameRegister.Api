LOTR Game Register API
Languages / Idiomas: English | Español

English Version
LOTR Game Register is a robust Backend solution for The Lord of the Rings: LCG players. It transforms flat Excel data into a normalized relational database for advanced match logging and statistical analysis.

🚀 Key Features

Normalized Architecture: 12-table relational schema designed to eliminate data redundancy.

Automatic Seeding: Custom init.sql script that auto-populates 103 heroes and 121 quests upon deployment.

Dockerized Environment: Instant setup via Docker Compose for both SQL Server and the API.

Advanced Analytics: Ready for complex queries (e.g., win rates per hero or sphere).

🛠️ Tech Stack

Backend: ASP.NET Core Web API (.NET 8)

Database: Microsoft SQL Server 2022

Infrastructure: Docker & Docker Compose

ORM: Entity Framework Core

📦 Quick Start

Clone the repository.

Run the command: docker-compose up -d

Access the API at: http://localhost:5000/swagger

📊 Data Insights (SQL Example)
SELECT h.Name, COUNT(gh.GameId) as MatchesPlayed
FROM Heroes h
JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Name
ORDER BY MatchesPlayed DESC;

🛣️ Roadmap
[x] Requirement documentation and DB Schema design.

[x] SQL initialization script (init.sql).

[x] Docker Compose configuration.

[ ] Unit Testing implementation.

[ ] Advanced Statistics Dashboard endpoint.

Versión en Español
LOTR Game Register es una solución Backend robusta diseñada para coleccionistas y jugadores del juego de cartas The Lord of the Rings: LCG. Permite registrar partidas, gestionar una base de datos de 103 héroes y más de 120 misiones, y analizar estadísticas de victoria.

🚀 Características Principales

Base de Datos Normalizada: Arquitectura relacional completa con 12 tablas, eliminando redundancias del Excel original.

Seeding Automático: Incluye un script init.sql que puebla el sistema con datos reales de la comunidad automáticamente.

Contenedorización: Despliegue inmediato mediante Docker, garantizando un entorno consistente.

Relaciones Complejas: Implementación de relaciones Many-to-Many entre Partidas y Héroes.

🛠️ Stack Tecnológico

Backend: ASP.NET Core Web API (.NET 8)

Base de Datos: Microsoft SQL Server 2022

Infraestructura: Docker & Docker Compose

ORM: Entity Framework Core

📦 Inicio Rápido

Clona el repositorio.

Ejecuta el comando: docker-compose up -d

Accede a la API en: http://localhost:5000/swagger

📊 Ejemplo de Consulta (SQL)
SELECT h.Name, COUNT(gh.GameId) as PartidasJugadas
FROM Heroes h
JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Name
ORDER BY PartidasJugadas DESC;

🛣️ Roadmap

[x] Documentación de requisitos y diseño de BD.

[x] Script de inicialización SQL (init.sql).

[x] Configuración de Docker Compose.

[ ] Implementación de Unit Testing.

[ ] Dashboard de estadísticas avanzado.

Author / Autor
Adrián

LinkedIn: linkedin.com/in/molinaarroyoadrian

Portfolio: https://github.com/adri13moar
