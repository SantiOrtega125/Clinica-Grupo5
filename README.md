# Clínica – Directorio de médicos

*Grupo 5 · Tema 4*
Tarea de problemas: Sistema web con autenticación y roles
Universidad de las Fuerzas Armadas ESPE – Departamento de Ciencias de la Computación

## Problema asignado

Una clínica registraba la información de sus médicos en hojas de cálculo que cualquiera podía abrir y modificar: se borraban registros por error, no se sabía quién cambió qué y los datos de una tabla no coincidían con los de la otra.

Esta aplicación web resuelve el problema:

- Los datos se validan antes de guardarse.
- Cada médico pertenece a una especialidad, elegida por su nombre en una lista desplegable.
- Solo las personas registradas pueden modificar información.
- El rol *Director* crea, edita y elimina; el rol *Paciente* solo consulta.

## Tecnologías

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core 10
- SQL Server Express
- ASP.NET Core Identity con roles

## Modelo de datos

| Tabla | Columnas | Relación |
|---|---|---|
| Especialidad (principal) | Id, Nombre, Descripcion (opcional) | Una especialidad tiene muchos médicos |
| Medico (dependiente) | Id, Nombre, NumeroLicencia, AniosExperiencia, EspecialidadId (FK) | Cada médico pertenece a una especialidad |

Además, la base de datos incluye las tablas de Identity: AspNetUsers, AspNetRoles, AspNetUserRoles, entre otras.

## Roles y permisos

| Acción | Visitante sin sesión | Paciente | Director |
|---|---|---|---|
| Ver lista y detalle de médicos y especialidades | Sí | Sí | Sí |
| Crear, editar y eliminar | No (lo envía al login) | No (acceso denegado) | Sí |

## Usuarios de prueba

Se crean automáticamente al iniciar la aplicación.

| Rol | Correo | Contraseña |
|---|---|---|
| Director | director@clinica.com | Director123* |
| Paciente | paciente@clinica.com | Paciente123* |

## Cómo ejecutar el proyecto

*Requisitos:* Visual Studio 2026 con el SDK de .NET 10, SQL Server Express y SQL Server Management Studio.

1. *Clonar el repositorio:* en Visual Studio, Git > Clonar repositorio, con la URL https://github.com/SantiOrtega125/AutoevaluacionFormativa_Grupo5.git.
2. *Ajustar la cadena de conexión* en appsettings.json con el nombre de su servidor SQL (el que aparece al conectarse en SSMS):

   json
   "ConnectionStrings": {
     "ClinicaContext": "Server=localhost\\SQLEXPRESS;Database=ClinicaDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   

3. *Crear la base de datos:* en Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes, ejecutar:

   powershell
   Update-Database
   

4. *Ejecutar* con F5. Los roles y los usuarios de prueba se crean solos al iniciar.

## Migraciones

| # | Migración | Qué hace |
|---|---|---|
| 1 | InitialCreate | Crea la tabla Medico |
| 2 | ValidacionesMedico | Aplica las longitudes y validaciones de Medico |
| 3 | AgregarEspecialidad | Crea la tabla Especialidad |
| 4 | RelacionEspecialidadMedico | Agrega EspecialidadId y la clave foránea hacia Especialidad |
| 5 | AgregarIdentity | Crea las tablas de usuarios y roles de Identity |

## Integrantes, turnos y aportes

| Turno | Integrante | Usuario de GitHub | Fases | Aporte |
|---|---|---|---|---|
| 1 | Santiago Ortega | SantiOrtega125 | Fase 1 · Proyecto y entidad Medico | Creación del proyecto MVC y del repositorio, entidad Medico con su CRUD, conexión a SQL Server y migración InitialCreate |
| 2 | Brayan Mendoza | skyshoto | Fase 2 · Validaciones y Fase 3 · Relación uno a muchos | DataAnnotations con mensajes en español, entidad Especialidad con su CRUD, clave foránea y lista desplegable de especialidades |
| 3 | Claudio Villalba | cdvillalba2 | Fase 4 · Identity | Identity con ApplicationUser, menú de sesión con _LoginPartial, registro, inicio y cierre de sesión, y protección de MedicosController con [Authorize] y [AllowAnonymous] |
| 4 | Milton Yuxi | MiltonYuxi | Fase 5 · Roles y componente autónomo | Roles Director y Paciente con usuarios de prueba, restricción por rol en ambos controladores, enlaces ocultos según el rol y rol visible en el saludo |
| 5 | Alan Neira | Niclik05 | Diseño visual y documentación | Rediseño de la página principal y de las vistas de Médicos y Especialidades, y README |

## Componente autónomo: restricción por rol

- Ambos controladores llevan [Authorize(Roles = "Director")], y las acciones Index y Details llevan [AllowAnonymous]. Solo el Director puede crear, editar o eliminar, aunque otro usuario escriba la dirección en el navegador.
- Las vistas usan User.IsInRole("Director") para ocultar los enlaces Crear, Editar y Eliminar a quien no tiene ese rol.
- El saludo del menú muestra el correo y el rol del usuario.

*Archivos modificados:* Controllers/MedicosController.cs, Controllers/EspecialidadesController.cs, Views/Medicos/Index.cshtml, Views/Medicos/Details.cshtml, Views/Especialidades/Index.cshtml, Views/Especialidades/Details.cshtml, Views/Shared/_LoginPartial.cshtml.
