# RecruitmentManager

## Instrucciones de Configuración de Recruitment Manager

### Requisitos Previos

- [SDK de .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o posterior (recomendado)

### Estructura del Proyecto

Esta solución consta de múltiples proyectos:

- **RecruitmentManager.API** - Servicio API backend
- **RecruitmentManager.Web** - Cliente web con Razor Pages
- Bibliotecas de soporte para infraestructura, aplicación, dominio y componentes compartidos

### Instrucciones de Configuración

#### 1. Clonar el Repositorio

git clone https://github.com/dacamapo95/RecruitmentManager.git

#### 2. Ejecutar el proyecto API

La API utiliza una base de datos SQLite en memoria que será inicializada automáticamente con datos de prueba cuando la aplicación inicie.

#### 3. Configuración del Cliente Web (RecruitmentManager.Web)

En el fichero `appsettings.Development.json` ajustar con la configuración de conexión a la API:

> **Nota:** Ajusta la dirección `ApiBaseUrl` si tu API se ejecuta en un puerto diferente.


#### 4. Ejecutar Ambos Proyectos con Visual Studio

1. Abre el archivo de solución en Visual Studio 2022
2. Haz clic derecho en la solución en el Explorador de Soluciones
3. Selecciona __Configurar proyectos de inicio__
4. Elige la opción __Múltiples proyectos de inicio__
5. Establece tanto `RecruitmentManager.API` como `RecruitmentManager.Web` en __Iniciar__
6. Haz clic en __Aplicar__ y __Aceptar__
7. Presiona F5 para iniciar la depuración de ambos proyectos

### Acceso a la Aplicación

- Interfaz Swagger de la API: [https://localhost:7186/swagger](https://localhost:7186/swagger)
- Cliente Web: [https://localhost:7156/](https://localhost:7156/)

### Notas


- La API utiliza una base de datos SQLite en memoria que se inicializa automáticamente al inicio
- No es necesario ejecutar migraciones de base de datos manualmente
- La base de datos se inicializa con candidatos, países, ciudades y estados de muestra
- Para consultar los candidatos registrados, navega a la pestaña "Candidates" en el menú principal de la aplicación web
