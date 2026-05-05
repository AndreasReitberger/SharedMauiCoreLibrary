using AndreasReitberger.Shared.Core.Database.Service;
using AndreasReitberger.Shared.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using SharedMauiCoreLibrary.Test.Utilities;
using SQLite;

namespace SharedMauiCoreLibrary.Test;

public partial class ToDoItem : ObservableObject
{
    #region Properties

    [ObservableProperty, PrimaryKey]
    public partial Guid Id { get; set; } = Guid.NewGuid();

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial int Tasks { get; set; }

    [ObservableProperty]
    public partial bool Completed { get; set; }

    #endregion
}

public class DatabaseTests
{
    readonly string datbasename = "test.db";
    SqliteDatabaseService? sqlite;
    SqlDatabaseService? sql;

    SecretAppSetting? appSecrets;

    [SetUp]
    public void Setup()
    {
        appSecrets = SecretAppSettingReaderExtension.ReadSectionFromConfigurationRoot<SecretAppSetting>("CoreTests");
        if (appSecrets is not null)
        {
            sqlite = new SqliteDatabaseService.SqliteConnectionBuilder()
                .WithLocalPath(Path.Combine(Directory.GetCurrentDirectory(), datbasename))
                .WithPassphrase(appSecrets.Passphrase)
                .Build();

            sql = new SqlDatabaseService.SqlConnectionBuilder()
                .WithConnectionString("")
                .Build();
        }
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
            await sqlite.ConnectAsync();
            CreateTableResult? res = await sqlite.CreateTableAsync<ToDoItem>();

            ToDoItem item = new()
            {
                Name = "Test",
                Tasks = 5,
                Completed = false
            };
            await sqlite.InsertWithChildrenAsync(item);

            ToDoItem? loaded = await sqlite.GetWithChildrenAsync<ToDoItem>(item.Id);
            Assert.That(item.Id, Is.EqualTo(loaded?.Id));
            Assert.That(item.Name, Is.EqualTo(loaded?.Name));
            Assert.That(item.Tasks, Is.EqualTo(loaded?.Tasks));
            Assert.That(item.Completed, Is.EqualTo(loaded?.Completed));

            int id = await sqlite.DeleteWithChildrenAsync<ToDoItem>(item.Id); 
            loaded = await sqlite.GetWithChildrenAsync<ToDoItem>(item.Id);
            Assert.That(loaded, Is.Null);

            await sqlite.TryDropTableAsync<ToDoItem>();
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
