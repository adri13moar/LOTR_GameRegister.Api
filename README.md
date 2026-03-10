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

### 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| **Backend** | ASP.NET Core Web API (.NET 8) |
| **Database** | Microsoft SQL Server 2022 |
| **Infrastructure** | Docker & Docker Compose |
| **ORM** | Entity Framework Core |

### 📦 Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/adri13moar/LOTR_GameRegister.Api.git
   cd LOTR_GameRegister.Api
   ```

2. **Start the application**
   ```bash
   docker-compose up -d
   ```

3. **Access the API**
   - Swagger UI: https://localhost:7052/swagger
   - API Base URL: https://localhost:7052

### 📊 Data Insights (SQL Example)

Get match statistics per hero:

```sql
SELECT 
    h.Name, 
    COUNT(gh.GameId) as MatchesPlayed
FROM Heroes h
JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Name
ORDER BY MatchesPlayed DESC;
```

### 🛣️ Roadmap

- [x] Requirement documentation and DB Schema design
- [x] SQL initialization script (`init.sql`)
- [x] Docker Compose configuration
- [ ] Unit Testing implementation
- [ ] Advanced Statistics Dashboard endpoint

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

### 🛠️ Stack Tecnológico

| Componente | Tecnología |
|-----------|-----------|
| **Backend** | ASP.NET Core Web API (.NET 8) |
| **Base de Datos** | Microsoft SQL Server 2022 |
| **Infraestructura** | Docker & Docker Compose |
| **ORM** | Entity Framework Core |

### 📦 Inicio Rápido

1. **Clona el repositorio**
   ```bash
   git clone https://github.com/adri13moar/LOTR_GameRegister.Api.git
   cd LOTR_GameRegister.Api
   ```

2. **Inicia la aplicación**
   ```bash
   docker-compose up -d
   ```

3. **Accede a la API**
   - Swagger UI: https://localhost:7052/swagger
   - URL Base de la API: https://localhost:7052

### 📊 Ejemplo de Consulta (SQL)

Obtén estadísticas de partidas por héroe:

```sql
SELECT 
    h.Name, 
    COUNT(gh.GameId) as PartidasJugadas
FROM Heroes h
JOIN GameHeroes gh ON h.Id = gh.HeroId
GROUP BY h.Name
ORDER BY PartidasJugadas DESC;
```

### 🛣️ Roadmap

- [x] Documentación de requisitos y diseño de BD
- [x] Script de inicialización SQL (`init.sql`)
- [x] Configuración de Docker Compose
- [ ] Implementación de Unit Testing
- [ ] Dashboard de estadísticas avanzado

---

## 👨‍💼 Author / Autor

**Adrián Molina Arroyo**

- 🔗 [LinkedIn](https://www.linkedin.com/in/molinaarroyoadrian)
- 📁 [GitHub Portfolio](https://github.com/adri13moar)

---

*Made with ❤️ for The Lord of the Rings: LCG community*