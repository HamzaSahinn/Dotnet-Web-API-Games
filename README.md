# Dotnet-Web-API-Games

## Project Description

- This repository contains a CRUD (Create, Read, Update, Delete) web API developed using .NET Core. The API facilitates the management of game-related data, allowing operations such as creating new game entries, retrieving specific game information, updating existing records, and deleting outdated data. (SOLID Princables applied)
- Technologies used .NET 8, ASP.NET, Entity Framework, Docker, SQL Server

## Starting SQL Server

```powershell
$sa_password="strong password"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm  --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

## Setting the connection string to secret manager

```powershell
$sa_password="strong password"
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```

## Start Project

```bash
cd Games.API
dotnet run
```
