# Project Management API

## Dependencies

- .NET SDK 5.0.101
- Docker

## How to run the API

First, ensure all dependencies are installed.
Second, issue the following commands in order:

```ps
.\scripts\create_db.ps1
cd src\ProjectManagement.API
dotnet run
```

## Scripts

The scripts folder contains three useful scripts which help prepare the database for the API. The first one `create_db.ps1` is a powershell script which will simply spin up a postgres docker container with the password already set to the correct value for development.

The second script `kill_db.ps1` will stop and remove the container completely. This is useful in conjunction with `create_db.ps1` if you wish to empty the database completely.

The third script `start_db.ps1` will start the container if it is stopped, for example after a restart of the Docker daemon.

## Migrations

The database migrations will be applied automatically to the development database after running the application.