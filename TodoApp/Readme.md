
### Install .Net Core SDK 2.1.500, runtime 2.1.6
```
.NET Core SDK (reflecting any global.json):
 Version:   2.1.500
 Commit:    b68b931422

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.17134
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\2.1.500\

Host (useful for support):
  Version: 2.1.6
  Commit:  3f4f8eebd8

.NET Core SDKs installed:
  2.1.201 [C:\Program Files\dotnet\sdk]
  2.1.202 [C:\Program Files\dotnet\sdk]
  2.1.403 [C:\Program Files\dotnet\sdk]
  2.1.500 [C:\Program Files\dotnet\sdk]

.NET Core runtimes installed:
  Microsoft.AspNetCore.All 2.1.5 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.All]
  Microsoft.AspNetCore.All 2.1.6 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.All]
  Microsoft.AspNetCore.App 2.1.5 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 2.1.6 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 2.0.7 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 2.0.9 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 2.1.5 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 2.1.6 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]

To install additional .NET Core runtimes or SDKs:
  https://aka.ms/dotnet-download

```
### Prepare database migration and setup
```
create appsettings.json
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update

```

### Create an appsettings.json with a connectionstring to db:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=TodoApp;"
  },
  "AllowedHosts": "*"
}
```

### generate script - dotnet ef migrations script :

``` 
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Todos] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [Checkmark] bit NOT NULL,
    CONSTRAINT [PK_Todos] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181119143212_InitialCreate', N'2.2.0-preview3-35497');

GO
```