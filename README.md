# Remitee Challenge Backend
---

## 🏗️ Descripción del Proyecto

Este proyecto es un **challenge técnico** para evaluación de habilidades backend .NET.  
Se implementó utilizando **Clean Architecture**, **CQRS**, **Mediator Pattern** y **FluentValidation**.  
Incluye:

- API REST con .NET 8
- Base de datos SQL Server
- Validaciones y manejo de errores profesional
- Tests unitarios y de integración
- Endpoints para gestión de libros

---

## 🛠 Tecnologías

| Tecnología            | Versión / Detalles                     |
|----------------------|----------------------------------------|
| .NET                  | 8.0                                    |
| C#                    | 10                                      |
| SQL Server            | 2019 / Local                            |
| MediatR               | CQRS / Mediator Pattern                |
| FluentValidation      | Validaciones de request                 |
| xUnit                 | Testing                                |
| Serilog               | Logging                                 |

---

## 📁 Estructura del Repositorio

Remitee.Challenge.Backend/
│ Remitee.Challenge.sln
│ README.md
│ scripts/
│ ├── schema.sql
│ └── seed.sql
│
├── Remitee.Challenge.API/
├── Remitee.Challenge.Application/
├── Remitee.Challenge.Domain/
├── Remitee.Challenge.Infrastructure/
└── Remitee.Challenge.Tests/

yaml
Copiar código

---

## 🚀 Ejecución Local

### 1️⃣ Requisitos

- SQL Server 2019/Express local
- Visual Studio 2022 o 2019
- .NET 8 SDK

### 2️⃣ Configuración de la base de datos

1. Abrir **SQL Server Management Studio (SSMS)**.
2. Ejecutar los scripts SQL que se encuentran en `/scripts`:

```sql
:r ./scripts/schema.sql
:r ./scripts/seed.sql
Verificar que la base de datos Remitee fue creada correctamente.

3️⃣ Configuración del proyecto API
La cadena de conexión está en Remitee.Challenge.API/appsettings.json:

json
Copiar código
{
  "ConnectionStrings": {
    "PrimaryDbConnection": "Server=DESKTOP-Q2EJO30\\SQLEXPRESS;Database=Remitee;User Id=apiuser;Password=Tomi20202025#;TrustServerCertificate=True;"
  },
  "CommandTimeout": 60,
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
Ajustar según tu entorno local si es necesario.

4️⃣ Ejecutar la API
Abrir la solución Remitee.Challenge.sln en Visual Studio.

Seleccionar Remitee.Challenge.API como proyecto de inicio.

Ejecutar en modo Debug o Release.

Swagger estará disponible en:

bash
Copiar código
http://localhost:7132/swagger/index.html
🏛 Arquitectura y decisiones técnicas
Clean Architecture: separa capas de API, Application, Domain e Infrastructure.

CQRS + Mediator (MediatR): separa comandos (modificación) y queries (lectura) para escalabilidad.

FluentValidation: asegura que los requests sean validados antes de procesarse.

Serilog: logging profesional con rolling files diarios.

Diagrama conceptual (simplificado):

scss
Copiar código
Controller (API)
       │
       ▼
Application (Commands / Queries + Validators)
       │
       ▼
Domain (Entities / Business Logic)
       │
       ▼
Infrastructure (DB, Repositorios, Email, FTP)
🧪 Testing
Tests unitarios y de integración con xUnit.

Ejecutar desde Visual Studio:
Test Explorer → Run All Tests.

Cobertura: Validación de servicios, handlers de CQRS y repositorios.

📄 Endpoints Principales
Método	Endpoint	Descripción	URL Swagger
GET	/api/Book/GetAllWithPagination	Obtener todos los libros con paginación	Swagger UI
POST	/api/Book/AddBook	Inserta un nuevo libro	Swagger UI
