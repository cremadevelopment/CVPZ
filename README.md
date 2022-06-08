# Introduction

**CV** for [Curriculum Vitae](https://en.wikipedia.org/wiki/Curriculum_vitae) is a written overview of a person's experience and other qualifications. **PZ** 'cause it sounds easy.

## What

Developers continuously need to keep their resumes current, documenting every project, engagement, and various details of accomplishments.  Many have created git repositories that can store versions of these resumes well. However, as the breadth of experience expands it becomes hard to manage the growing amount of data in a single document. There are places to manage resume information, but these tools do not allow management of versions of the individual projects or the ability to query history to aid in highlighting specific experience.

## Why

We are hoping to inspire community involvement by building a simple and useful tool and to create an open space for anyone who wants to be involved to come and play. As a community we can expose each other to our favorite technologies and libraries, build relationships and refine our soft skills. Ultimately this should affect our local development community in many positive ways.

## How

By having one common goal to work toward, we will share ideas and continually move the project forward.  We will start with the setup of some infrastructure and then work on basic requirements.  The possibilities are endless with collaborative effort!

## Getting Started

1. Clone the repository: `git clone https://github.com/cremadevelopment/CVPZ.git`
1. Edit the database connection string variables in `.\scripts\init-secrets.ps1` for your prefered database instance
1. Setup .Net Secrets: `. .\scripts\init-secrets.ps1`
1. Enter YourName

## Insomnia

This project includes a json export of an Insomnia collection. This collection is setup with a folder for each web api controller, calls to exercise the api. Please contribute by adding more calls as the api expands with new features.

### Setup Insomnia

1. Download and install [Insomnia](https://insomnia.rest/)
1. Create a new project in Insomnia
1. Go to `Import/Export`
1. Select `Import Data` then `From File`
1. Select the file in the source directory `CVPZ\Insomnia\Insomnia_CVPZ.json`

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
