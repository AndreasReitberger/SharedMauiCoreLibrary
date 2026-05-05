# 📋 SharedMauiCoreLibrary.Database
A shared library, which enables local (SQLite) or remote (SQL) database connections of your .NET MAUI applications.

# 📦 Nuget
Get the latest version from nuget.org<br>
[![NuGet](https://img.shields.io/nuget/v/SharedMauiCoreLibrary.Database.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SharedMauiCoreLibrary.Database/)
[![NuGet](https://img.shields.io/nuget/dt/SharedMauiCoreLibrary.Database.svg)](https://www.nuget.org/packages/SharedMauiCoreLibrary.Database)

## 📄 Usage

### SQL

```cs
sql = new SqlDatabaseService.SqlConnectionBuilder()
    .WithConnectionString("connection string...")
    .Build();
```

### SQLITE


```cs
sqlite = new SqliteDatabaseService.SqliteConnectionBuilder()
    .WithLocalPath("database.db")
    .WithPassphrase("your_passphrase")
    .Build();
```


