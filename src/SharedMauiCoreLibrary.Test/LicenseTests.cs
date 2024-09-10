using AndreasReitberger.Shared.Core.Licensing;
using AndreasReitberger.Shared.Core.Licensing.Enums;
using AndreasReitberger.Shared.Core.Licensing.Interfaces;
using AndreasReitberger.Shared.Core.Licensing.Models;
using AndreasReitberger.Shared.Core.Utilities;
using SharedMauiCoreLibrary.Test.Utilities;

namespace SharedMauiCoreLibrary.Test;

public class Tests
{
    string licenseUri = "andreas-reitberger.de";
    LicenseManager? manager;
    LicenseInfo? info;
    SecretAppSetting? appSecrets;

    [SetUp]
    public void Setup()
    {
        appSecrets = new SecretAppSettingReader().ReadSectionFromConfigurationRoot<SecretAppSetting>("CoreTests");

        manager = new LicenseManager.LicenseManagerConnectionBuilder()
            .WithLicenseServer(serverAddress: licenseUri, port: null, https: true)
            .Build();
        if (appSecrets == null)
            throw new ArgumentNullException(nameof(appSecrets));
        info = new LicenseInfo.LicenseInfoBuilder()
            .WithProductIdentifier(appSecrets.ProductCode)
            .WithLicense(appSecrets.License)
            .WithDomain(appSecrets.Domain)
            .WithOptions(new LicenseOptions()
            {
                ProductName = "3D Print Cost Calculator",
                ProductIdentifier = appSecrets.ProductCode,
                LicenseCheckPattern = "^AR-((\\w{8})-){2}(\\w{8})$",
            })
            .Build();
    }

    [Test]
    public async Task TestLicenseServer()
    {
        try
        {
            if (manager is null)
                throw new ArgumentNullException(nameof(manager));
            ILicenseQueryResult result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
            Assert.That(result?.Success == true);

            result = await manager.DeactivateLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
            Assert.That(result?.Success == true);

            result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
            Assert.That(result?.Success == false);

            result = await manager.ActivateLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
            Assert.That(result?.Success == true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public async Task TestEnvatoLicenseCheck()
    {
        try
        {
            if (appSecrets is null)
                throw new ArgumentNullException(nameof(appSecrets));
            manager = new LicenseManager.LicenseManagerConnectionBuilder()
            .WithLicenseServer(serverAddress: "api.envato.com/v3/market/author/sale", port: null, https: true)
            .WithAccessToken(appSecrets.AccessToken)
            .Build();
            info = new LicenseInfo.LicenseInfoBuilder()
                .WithProductIdentifier(appSecrets.ItemId)
                .WithLicense(appSecrets.PurchaseCode)
                .WithOptions(new LicenseOptions()
                {
                    ProductName = "3D Print Cost Calculator",
                    ProductIdentifier = appSecrets.ItemId,
                    LicenseCheckPattern = "^(\\w{8})-((\\w{4})-){3}(\\w{12})$",
                })
                .Build();

            ILicenseQueryResult result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.Envato);
            Assert.That(result?.Success == true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
