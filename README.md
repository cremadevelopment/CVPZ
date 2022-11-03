# Introduction

Welcome to CVPZ...

## Getting Started

1. Clone the repository: `git clone https://cremadevelopment@dev.azure.com/cremadevelopment/CVPZ/_git/CVPZ`
1. Edit the database connection string variables in `.\scripts\init-secrets.ps1` for your prefered database instance
1. Setup .Net Secrets: `. .\scripts\init-secrets.ps1`
1. Enter YourName

## Database

Persistence of the data for this project is currently setup to use SQL Server. The connection string is configured through the `init-secrets.ps1` script as discussed in [Getting Started](#getting-started). The database schema is managed through Entity Framework code first migrations.

We recommend using the linux docker instance of sql, becuase who wants to install and configure SQL server?

```PowerShell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" `
   -p 8433:1433 --name sqlCVPZ --hostname sqlCVPZ `
   -d mcr.microsoft.com/mssql/server:2019-latest
```

For further reference [Microsoft Quickstart for SQL in Docker](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-powershell)

### Create Migration

If the `CVPZ.Domain` objects are modified resulting in a change to the database schema, create a new migration by running `. .\scripts\CreateMigration.ps1` and entering a name for the migration when prompted.

### Update Database

To update an existing database to match the current version of the database schema run `. .\scripts\UpdateDatabase.ps1`.
