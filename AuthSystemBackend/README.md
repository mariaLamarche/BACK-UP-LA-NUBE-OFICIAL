# Auth System Backend

Una API REST desarrollada con ASP.NET Core 9.0 que proporciona un sistema completo de autenticación y autorización usando JWT (JSON Web Tokens) y Entity Framework Core.

## 🚀 Características

- **Autenticación JWT**: Sistema seguro de tokens para autenticación
- **Entity Framework Core**: ORM para manejo de base de datos
- **SQL Server**: Base de datos principal
- **Encriptación BCrypt**: Hash seguro de contraseñas
- **CORS configurado**: Para comunicación con frontend Angular
- **Swagger/OpenAPI**: Documentación automática de la API
- **Migraciones**: Sistema de versionado de base de datos
- **Validación robusta**: Validación de modelos y datos

## 📋 Prerrequisitos

Antes de comenzar, asegúrate de tener instalado:

- **.NET 9.0 SDK** o superior
- **SQL Server** (LocalDB, Express, o Developer Edition)
- **Visual Studio 2022** o **Visual Studio Code** (recomendado)
- **SQL Server Management Studio** (opcional, para gestión de BD)

### Instalación de .NET 9.0 SDK

Descarga e instala desde: [https://dotnet.microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0)

Verifica la instalación:
```bash
dotnet --version
```

## 🗄️ Configuración de Base de Datos

### Opción 1: SQL Server LocalDB (Recomendado para desarrollo)

LocalDB viene incluido con Visual Studio y es perfecto para desarrollo local.

1. **Verifica que LocalDB esté instalado**:
```bash
sqllocaldb info
```

2. **Actualiza la cadena de conexión** en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AuthSystemDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Opción 2: SQL Server Express/Developer

1. **Instala SQL Server Express** desde: [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

2. **Actualiza la cadena de conexión**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR\\SQLEXPRESS;Database=AuthSystemDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Opción 3: SQL Server en la Nube (Azure SQL, AWS RDS, etc.)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:tu-servidor.database.windows.net,1433;Initial Catalog=AuthSystemDB;Persist Security Info=False;User ID=tu-usuario;Password=tu-contraseña;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

## 🛠️ Instalación y Configuración

1. **Clona el repositorio** (si no lo has hecho ya):
```bash
git clone <url-del-repositorio>
cd AuthSystemBackend
```

2. **Restaura los paquetes NuGet**:
```bash
dotnet restore
```

3. **Actualiza la cadena de conexión** en `appsettings.json` con tu configuración de base de datos.

4. **Configura la clave secreta JWT** en `appsettings.json`:
```json
{
  "JwtSettings": {
    "SecretKey": "TuClaveSecretaSuperSeguraQueTengaAlMenos32Caracteres!",
    "Issuer": "AuthSystemBackend",
    "Audience": "AuthSystemFrontend",
    "ExpirationInMinutes": 60
  }
}
```

**⚠️ IMPORTANTE**: Cambia la clave secreta en producción por una más segura.

## 🗃️ Configuración de Base de Datos

### Crear la Base de Datos

1. **Ejecuta las migraciones** para crear la base de datos:
```bash
dotnet ef database update
```

Si no tienes las herramientas de Entity Framework instaladas globalmente:
```bash
dotnet tool install --global dotnet-ef
```

### Verificar la Base de Datos

La base de datos `AuthSystemDB` se creará automáticamente con las siguientes tablas:
- `Users`: Información de usuarios
- `__EFMigrationsHistory`: Historial de migraciones

## 🏃‍♂️ Ejecución

### Modo de desarrollo

```bash
dotnet run
# o
dotnet watch run
```

La API estará disponible en:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:7000`
- **Swagger UI**: `https://localhost:7000/swagger`

### Compilación para producción

```bash
dotnet build --configuration Release
dotnet publish --configuration Release
```

## 📁 Estructura del Proyecto

```
AuthSystemBackend/
├── Controllers/             # Controladores de la API
│   ├── AuthController.cs   # Endpoints de autenticación
│   └── HomeController.cs   # Controlador principal
├── Data/                   # Contexto de base de datos
│   └── ApplicationDbContext.cs
├── Models/                 # Modelos de datos
│   ├── User.cs            # Modelo de usuario
│   ├── LoginRequest.cs    # Modelo para login
│   ├── RegisterRequest.cs # Modelo para registro
│   └── ErrorViewModel.cs  # Modelo de errores
├── Services/              # Servicios de negocio
│   └── AuthService.cs     # Lógica de autenticación
├── Migrations/            # Migraciones de Entity Framework
├── Views/                 # Vistas MVC (opcional)
├── wwwroot/              # Archivos estáticos
├── Properties/           # Configuración del proyecto
├── Program.cs            # Punto de entrada de la aplicación
├── appsettings.json      # Configuración general
└── appsettings.Development.json # Configuración de desarrollo
```

## 🔐 Endpoints de la API

### Autenticación

#### POST `/api/auth/register`
Registra un nuevo usuario.

**Request Body**:
```json
{
  "email": "usuario@ejemplo.com",
  "password": "contraseña123",
  "confirmPassword": "contraseña123",
  "firstName": "Juan",
  "lastName": "Pérez"
}
```

**Response** (200 OK):
```json
{
  "success": true,
  "message": "Usuario registrado exitosamente",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "email": "usuario@ejemplo.com",
    "firstName": "Juan",
    "lastName": "Pérez"
  }
}
```

#### POST `/api/auth/login`
Inicia sesión con un usuario existente.

**Request Body**:
```json
{
  "email": "usuario@ejemplo.com",
  "password": "contraseña123"
}
```

**Response** (200 OK):
```json
{
  "success": true,
  "message": "Login exitoso",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "email": "usuario@ejemplo.com",
    "firstName": "Juan",
    "lastName": "Pérez"
  }
}
```

#### GET `/api/auth/me`
Obtiene información del usuario autenticado.

**Headers**:
```
Authorization: Bearer <token>
```

**Response** (200 OK):
```json
{
  "success": true,
  "user": {
    "id": 1,
    "email": "usuario@ejemplo.com",
    "firstName": "Juan",
    "lastName": "Pérez"
  }
}
```

## 🔧 Configuración Detallada

### JWT Configuration

En `appsettings.json`:
```json
{
  "JwtSettings": {
    "SecretKey": "TuClaveSecretaSuperSeguraQueTengaAlMenos32Caracteres!",
    "Issuer": "AuthSystemBackend",
    "Audience": "AuthSystemFrontend",
    "ExpirationInMinutes": 60
  }
}
```

### CORS Configuration

El backend está configurado para permitir peticiones desde el frontend Angular:
- **Origen permitido**: `http://localhost:4200` (desarrollo)
- **Métodos permitidos**: Todos
- **Headers permitidos**: Todos

Para producción, actualiza el origen en `Program.cs`:
```csharp
policy.WithOrigins("https://tu-dominio-frontend.com")
```

## 🗄️ Migraciones de Base de Datos

### Crear una nueva migración

```bash
dotnet ef migrations add NombreDeLaMigracion
```

### Aplicar migraciones

```bash
dotnet ef database update
```

### Revertir migración

```bash
dotnet ef database update MigracionAnterior
```

### Eliminar migración (antes de aplicar)

```bash
dotnet ef migrations remove
```

## 🧪 Pruebas

### Ejecutar pruebas

```bash
dotnet test
```

### Probar la API con Swagger

1. Ejecuta la aplicación: `dotnet run`
2. Ve a `https://localhost:7000/swagger`
3. Usa la interfaz de Swagger para probar los endpoints

### Probar con Postman/Insomnia

Importa la colección de Postman o usa los ejemplos de requests proporcionados en la documentación de Swagger.

## 📦 Dependencias Principales

- **Microsoft.EntityFrameworkCore.SqlServer**: Proveedor de SQL Server para EF Core
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autenticación JWT
- **BCrypt.Net-Next**: Encriptación de contraseñas
- **Swashbuckle.AspNetCore**: Documentación automática de API (Swagger)

## 🚀 Despliegue

### Preparación para Producción

1. **Actualiza la configuración**:
   - Cambia la cadena de conexión por la de producción
   - Actualiza la clave secreta JWT
   - Configura CORS para el dominio de producción

2. **Variables de entorno** (recomendado):
```bash
export ConnectionStrings__DefaultConnection="Server=prod-server;Database=AuthSystemDB;..."
export JwtSettings__SecretKey="TuClaveSecretaDeProduccionSuperSegura"
```

3. **Compila y publica**:
```bash
dotnet publish --configuration Release --output ./publish
```

### Despliegue en IIS

1. Instala el **ASP.NET Core Hosting Bundle**
2. Configura el sitio web en IIS
3. Apunta al directorio de publicación
4. Configura las variables de entorno

### Despliegue en Docker

Crea un `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AuthSystemBackend.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthSystemBackend.dll"]
```

## 🔒 Seguridad

### Medidas Implementadas

- **Encriptación BCrypt**: Para hash de contraseñas
- **JWT Tokens**: Autenticación stateless segura
- **Validación de modelos**: Validación automática de datos de entrada
- **HTTPS**: Configurado para producción
- **CORS**: Configuración restrictiva

### Recomendaciones Adicionales

1. **Rate Limiting**: Implementa límites de velocidad
2. **Logging**: Configura logging detallado
3. **Monitoring**: Implementa monitoreo de la aplicación
4. **Backup**: Configura backups automáticos de la base de datos

## 🐛 Solución de Problemas

### Problemas Comunes

1. **Error de conexión a base de datos**:
   - Verifica que SQL Server esté ejecutándose
   - Comprueba la cadena de conexión
   - Verifica permisos de usuario

2. **Error de migración**:
   ```bash
   dotnet ef database update --verbose
   ```

3. **Error de JWT**:
   - Verifica la configuración de JWT en `appsettings.json`
   - Comprueba que la clave secreta tenga al menos 32 caracteres

4. **Error de CORS**:
   - Verifica la configuración CORS en `Program.cs`
   - Comprueba que el frontend esté en el origen permitido

### Logs de Debugging

Habilita logging detallado en `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Notas de Desarrollo

- **ASP.NET Core 9.0**: Utiliza la versión más reciente
- **Entity Framework Core**: ORM moderno y eficiente
- **JWT**: Tokens seguros y stateless
- **Minimal APIs**: Estructura moderna de ASP.NET Core
- **Dependency Injection**: Arquitectura limpia y testeable

## 📞 Soporte

Si tienes problemas o preguntas:
1. Revisa la documentación de ASP.NET Core
2. Verifica los logs de la aplicación
3. Consulta la documentación de Entity Framework Core
4. Abre un issue en el repositorio

---

**Versión**: 1.0.0  
**Framework**: ASP.NET Core 9.0  
**Base de Datos**: SQL Server  
**Última actualización**: Enero 2025
