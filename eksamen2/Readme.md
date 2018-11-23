
```
install .net core sdk 2.1.500
create appsettings.json
dotnet restore
dotnet ef migrations add InitialCreate //  opretter Migrationsmappen med 3 filer
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