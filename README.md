# PortalCOSIE

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet\&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet\&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-6DB33F)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Azure%20SQL-CC2927?logo=microsoftsqlserver\&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-App%20Service%20%7C%20Blob%20Storage-0078D4?logo=microsoftazure\&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap\&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Onion%20%2B%20CQRS-success)
![Status](https://img.shields.io/badge/Status-Academic%20Project-blue)

**PortalCOSIE** es un sistema web desarrollado en **.NET 8 con ASP.NET Core MVC** para gestionar trámites de la **Comisión de Situación Escolar (COSIE-CTCE)** de la **UPIITA - IPN**.

El portal centraliza solicitudes, documentos, revisiones, sesiones COSIE, estadísticas y firma electrónica dentro de una solución institucional, segura y mantenible.

---

## ¿Por qué se hizo?

En la UPIITA, los trámites COSIE implican integración de expedientes, revisión documental y seguimiento administrativo. Cuando este proceso se realiza de forma manual o semidigital, se presentan problemas como:

* Pérdida de trazabilidad del expediente.
* Dependencia de documentos físicos.
* Falta de seguimiento claro para el alumno.
* Carga operativa para Gestión Escolar.
* Poca disponibilidad de estadísticas.
* Riesgos de integridad y no repudio documental.

**PortalCOSIE** fue desarrollado para digitalizar este flujo y facilitar el seguimiento de los dictámenes internos CTCE.

---

## Funcionalidades

* Registro e inicio de sesión de usuarios.
* Autorización por roles.
* Solicitud de trámites CTCE.
* Carga de documentos PDF.
* Revisión documental.
* Corrección de documentos observados.
* Asignación de trámites al personal.
* Conclusión de expedientes.
* Gestión de sesiones COSIE.
* Administración de carreras y unidades de aprendizaje.
* Dashboard estadístico.
* Firma electrónica de documentos.
* Almacenamiento de archivos en Azure Blob Storage.

---

## Roles

| Rol               | Funciones principales                                                              |
| ----------------- | ---------------------------------------------------------------------------------- |
| **Alumno**        | Solicitar trámites, cargar documentos, firmar archivos y consultar estado.         |
| **Personal**      | Revisar expedientes, validar documentos, emitir observaciones y concluir trámites. |
| **Administrador** | Gestionar usuarios, catálogos, periodos, sesiones COSIE y estadísticas.            |

---

## Arquitectura

El sistema utiliza **Onion Architecture** con separación por capas.

```txt
PortalCOSIE.Web
│  ASP.NET MVC, Controllers, Views, Bootstrap
│
PortalCOSIE.Application
│  Commands, Queries, Handlers, DTOs, Mediator
│
PortalCOSIE.Domain
│  Entities, Rules, Enumerations, Domain Exceptions
│
PortalCOSIE.Infrastructure
   EF Core, Identity, Repositories, Azure Blob, Crypto, Email
```

También implementa **CQRS** separando operaciones de escritura mediante `Commands` y operaciones de lectura mediante `Queries`.

---

## Stack técnico

| Área          | Tecnología                                     |
| ------------- | ---------------------------------------------- |
| Backend       | .NET 8, ASP.NET Core MVC                       |
| Frontend      | Razor Views, Bootstrap 5, JavaScript           |
| ORM           | Entity Framework Core                          |
| Base de datos | SQL Server / Azure SQL Database                |
| Identidad     | ASP.NET Identity                               |
| Archivos      | Azure Blob Storage                             |
| Cloud         | Azure App Service                              |
| Arquitectura  | Onion Architecture, CQRS                       |
| Patrones      | Repository, Unit of Work, Mediator propio      |
| Seguridad     | Roles, validación de acceso, firma electrónica |

---

## Módulos principales

### Usuarios

Gestión de alumnos, personal y administradores.

* Registro de alumnos.
* Registro de personal.
* Edición de perfiles.
* Activación o restricción de cuentas.
* Cambio y recuperación de contraseña.

### Trámites CTCE

Flujo principal del sistema.

* Solicitar trámite.
* Revisar documentos.
* Corregir expediente.
* Asignar personal.
* Cancelar trámite.
* Concluir trámite.

### Documentos

Gestión de archivos asociados al expediente.

* Carga de PDF.
* Descarga individual.
* Descarga de expediente completo.
* Validación de acceso por rol.
* Almacenamiento en Azure Blob Storage.

### Dashboard

Indicadores para Gestión Escolar.

* Estado de trámites.
* Estado de documentos.
* Solicitudes por carrera.
* Unidades más reprobadas.
* Roles de alumnos.

### Sesiones COSIE

Configuración de sesiones y fechas de recepción documental.

---

## Firma electrónica

PortalCOSIE integra firma electrónica para reforzar la integridad y no repudio de los documentos.

Flujo general:

```txt
PDF cargado
   ↓
Lectura del archivo
   ↓
Firma con llave privada
   ↓
Almacenamiento en Azure Blob Storage
   ↓
Registro de metadatos en base de datos
```

Servicios involucrados:

* `ICriptoService`
* `IStorageService`
* `IUnitOfWork`
* Repositorios de trámite y usuario

---

## Configuración

El proyecto utiliza variables de entorno o archivos de configuración locales para manejar secretos.

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PortalCOSIE;User Id=[USER];Password=[PASSWORD];TrustServerCertificate=True;",
    "AzureBlobStorage": "DefaultEndpointsProtocol=https;AccountName=[ACCOUNT];AccountKey=[KEY];EndpointSuffix=core.windows.net"
  },
  "AzureBlob": {
    "ContainerName": "[CONTAINER_NAME]"
  },
  "Smtp": {
    "Host": "smtp.gmail.com",
    "Port": "587",
    "User": "[EMAIL]",
    "Pass": "[APP_PASSWORD]",
    "From": "[EMAIL]"
  }
}
```
## Instalación local

### Requisitos

* Visual Studio 2022
* .NET SDK 8.x
* SQL Server
* SQL Server Management Studio
* Azure Storage Account
* Git

### Clonar el repositorio

```bash
git clone https://github.com/sau-c/PortalCOSIE.git
cd PortalCOSIE
```

### Restaurar dependencias

```bash
dotnet restore
```

### Compilar solución

```bash
dotnet build
```

### Aplicar migraciones

```bash
dotnet ef database update --project src/PortalCOSIE.Infrastructure --startup-project src/PortalCOSIE.Web
```

### Ejecutar

```bash
dotnet run --project src/PortalCOSIE.Web
```

---

## Despliegue

El proyecto está preparado para ejecutarse en Azure.

Servicios utilizados:

* **Azure App Service** para la aplicación web.
* **Azure SQL Database** para persistencia.
* **Azure Blob Storage** para documentos PDF.
* **Application Settings** para variables de entorno.

Flujo básico:

```txt
GitHub / Visual Studio
        ↓
Azure App Service
        ↓
Azure SQL Database
        ↓
Azure Blob Storage
```

---

## Seguridad

* Autenticación con ASP.NET Identity.
* Autorización por roles.
* Validación de acceso a documentos.
* Uso de DTOs para evitar exposición directa del dominio.
* Separación de responsabilidades por capas.
* Variables de entorno para secretos.
* Firma electrónica para integridad documental.
* Eliminación lógica en catálogos y entidades relevantes.

---

## Capturas

Crear la carpeta:

```txt
docs/screenshots/
```

Sugerencia de archivos:

```txt
login.png
dashboard.png
solicitud-ctce.png
revision-documental.png
detalle-tramite.png
```

Ejemplo:

```md
![Login](docs/screenshots/login.png)
![Dashboard](docs/screenshots/dashboard.png)
![Solicitud CTCE](docs/screenshots/solicitud-ctce.png)
```

---

## Estado del proyecto

Proyecto académico desarrollado como trabajo terminal de Ingeniería Telemática.

* Arquitectura base implementada.
* Módulos principales desarrollados.
* Gestión documental integrada.
* Firma electrónica implementada.
* Despliegue en Azure documentado.

---

## Autor

**Saul Castañeda**
Ingeniero Telemático
Desarrollador .NET

GitHub: [sau-c](https://github.com/sau-c)

---

## Institución

**Instituto Politécnico Nacional**
**Unidad Profesional Interdisciplinaria en Ingeniería y Tecnologías Avanzadas**
**Ingeniería Telemática**

---

## Licencia

Proyecto académico desarrollado con fines educativos y de investigación.

Todos los derechos reservados © Saul Castañeda.
