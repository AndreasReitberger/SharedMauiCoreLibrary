using AndreasReitberger.Shared.Core.Database.Service;
using AndreasReitberger.Shared.Core.Utilities;
using SharedMauiCoreLibrary.Test.Utilities;

namespace SharedMauiCoreLibrary.Test;

public class DatabaseTests
{
    SqliteDatabaseService? sqlite;
    SqlDatabaseService? sql;

    SecretAppSetting? appSecrets;

    [SetUp]
    public void Setup()
    {
        appSecrets = new SecretAppSettingReader().ReadSectionFromConfigurationRoot<SecretAppSetting>("DatabaseTests");
        sql = new SqlDatabaseService.SqlConnectionBuilder()
            .WithConnectionString("")
            .Build();
        sqlite = new SqliteDatabaseService.SqliteConnectionBuilder()
            .WithLocalPath("")
            .WithPassphrase("")
            .Build();
    }

    [Test]
    public async Task SqlTest()
    {
        try
        {
            if (sql is null)
                throw new ArgumentNullException(nameof(sql));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public async Task SqliteTest()
    {
        try
        {
            if (sqlite is null)
                throw new ArgumentNullException(nameof(sqlite));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        if (sqlite is not null)
        {
            await sqlite.DisconnectAsync();
            sqlite?.Dispose();
        }
        sql?.Dispose();
    }
}
