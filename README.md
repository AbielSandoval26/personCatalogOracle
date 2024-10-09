# Person Catalog

Este es un proyecto de API RESTful y una aplicación Blazor WebAssembly para la gestión de un catálogo de personas. La API permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) en un conjunto de datos de personas.

## Requisitos Previos

- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0) instalado en tu máquina.
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/) instalado.

## Estructura del Proyecto

- **personCatalogAPI**: Proyecto de la API que gestiona las operaciones relacionadas con el catálogo de personas.
- **personCatalog**: Proyecto de la aplicación Blazor WebAssembly que consume la API.

## Pasos para la Ejecución de la Aplicación

### 1. Clonar el Repositorio

```bash
https://github.com/AbielSandoval26/personCatalogOracle.git
cd personCatalog
```

### 2. Configurar la Base de Datos

- Asegúrate de tener una base de datos Oracle disponible.
- Actualiza la cadena de conexión en el archivo appsettings.json de personCatalogAPI para apuntar a tu base de datos Oracle.

### 3. Ejecutar la API

- Abre la solución en Visual Studio 2022.
- Compila el proyecto personCatalogAPI.

Ejecuta el proyecto (F5) o usa el siguiente comando en la terminal:

```bash
dotnet run --project personCatalogAPI
```

### 4. Ejecutar la Aplicación Blazor

- Abre otro proyecto de la solución (personCatalog).
- Compila y ejecuta el proyecto (F5) o usa el siguiente comando en la terminal:

```bash
dotnet run --project personCatalog
```
