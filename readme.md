<h1 style="border-bottom: 0">
    ASPIRE
</h1>

<h2 style="border-bottom: 0">
    The Full Suite Of Project KONGOR Services
    <br/>
    Architected As An Open-Source Cloud-Ready Distributed Application
</h2>

<h3 style="border-bottom: 0">
    If you would like to support the development of this project and buy me a coffee, please consider the following options: <a href="https://github.com/sponsors/K-O-N-G-O-R">GitHub Sponsors</a>, <a href="https://paypal.me/MissingLinkMedia">PayPal</a>. 💚
</h3>

<br/><hr/>

<h2 align="center">Concise Instructions For Developers</h2>

Non-Code Dependencies
* SQL Server: https://www.microsoft.com/en-gb/sql-server/sql-server-downloads (preferably Developer Edition)
* Docker Desktop: https://www.docker.com/products/docker-desktop/

<br/>

Run In Development

```powershell
# In The Context Of The Solution Directory
dotnet run --project ASPIRE.AppHost --launch-profile "Project KONGOR Development"
```

<br/>

Run In Production

```powershell
# In The Context Of The Solution Directory
dotnet run --project ASPIRE.AppHost --launch-profile "Project KONGOR Production"
```

<br/>

NOTE: If no launch profile is specified, then the first profile in `launchSettings.json` will be used. Normally, this default profile is a development profile, which allows developers to run the project quickly by executing just `dotnet run` without specifying a launch profile.

<br/>

Create A Database Schema Migration

1. install the Entity Framework Core CLI by executing `dotnet tool install --global dotnet-ef` or update it by executing `dotnet tool update --global dotnet-ef`
2. in the context of the solution directory, execute `dotnet ef migrations add {MigrationName} --project MERRICK.Database.Manager`

<br/>

Update The Database

```powershell
# Development Database
# In The Context Of The Solution Directory
$ENV:ASPNETCORE_ENVIRONMENT = "Development"
dotnet ef database update --project MERRICK.Database.Manager
```

```powershell
# Production Database
# In The Context Of The Solution Directory
$ENV:ASPNETCORE_ENVIRONMENT = "Production"
dotnet ef database update --project MERRICK.Database.Manager
```

<br/><hr/>

<h2 align="center">Comprehensive Instructions For Non-Developers</h2>

1. ??? (coming soon™)
2. play HoN
<br/>
