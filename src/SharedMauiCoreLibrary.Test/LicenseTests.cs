using AndreasReitberger.Shared.Core.Licensing;
using AndreasReitberger.Shared.Core.Licensing.Enums;

namespace SharedMauiCoreLibrary.Test;

public class Tests
{
    string licenseUri = "andreas-reitberger.de";
    LicenseManager manager;
    LicenseInfo info;

    [SetUp]
    public void Setup()
    {
        manager = new LicenseManager.LicenseManagerConnectionBuilder()
            .WithLicenseServer(serverAddress: licenseUri, port: null, https: true)
            .WithOptions(new AndreasReitberger.Shared.Core.Licensing.Models.LicenseOptions()
            {
                ProductName = "3D Print Cost Calculator",
                ProductIdentifier = "AR-3DDKKV2",
                LicenseCheckPattern = "^AR-((\\w{8})-){2}(\\w{8})$",
            })
            .Build();
        info = new LicenseInfo()
        {
            Application = "AR-3DDKKV2",
        };
    }

    [Test]
    public async Task Test1()
    {
        var result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
        Assert.Pass();
    }
}
