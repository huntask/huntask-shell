# Dotnet Microservices Template

[![🚀 Build](https://github.com/hutorovmv/dotnet-microservices-template/actions/workflows/docker-compose-run.yml/badge.svg)](https://github.com/hutorovmv/dotnet-microservices-template/actions/workflows/docker-compose-run.yml)

## Overview

A modern .NET microservices starter template. This solution demonstrates best practices for building, running, and maintaining distributed services with .NET 9, Docker, and GitHub Actions.

### Key Technologies

- **.NET 9** (Minimal API)
- **NuGet Package** (shared utilities)
- **PostgreSQL** (database)
- **Entity Framework** (ORM)
- **Docker & Docker Compose** (containerization)
- **PgAdmin** (database management)
- **GitHub Actions** (CI/CD)
- **Copilot** (copilot-instructions, MCP)


### Services

- **Huntask.Identity.Service** – User authentication and management
- **Huntask.Tasks.Service** – Todo management API
- **PostgreSQL** – Main database
- **PgAdmin** – Database GUI (development only)

All services run in isolated Docker containers. Start the full stack with Docker Compose.

### Wiki

For more information check repo [Wiki](https://github.com/hutorovmv/dotnet-microservices-template/wiki)

**Important:**
 - [Run the containers](https://github.com/hutorovmv/dotnet-microservices-template/wiki/Docker)
 - [Access Resources](https://github.com/hutorovmv/dotnet-microservices-template/wiki/Resources)

## Screenshots

![Identity Service](https://github.com/user-attachments/assets/e75aa1cc-b4b8-4c41-a7a3-085ed14fc1e0)
![TodoList Service](https://github.com/user-attachments/assets/72f90a7e-35cd-45e7-85d8-612efc688052)
![PgAdmin](https://github.com/user-attachments/assets/303c0fae-793d-440a-bf86-f4f7477343bb)
