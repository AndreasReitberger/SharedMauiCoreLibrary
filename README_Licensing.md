# SharedMauiCoreLibrary.Licensing
A shared library, which enables licensing of your .NET MAUI applications.

# Dependencies
This extension needs a WooCommerce powered store and the WP Software License Plugin (https://wpsoftwarelicense.com/)

# Documentation
Learn more here:
https://andreas-reitberger.de/en/docs/programmieren/net-maui-basis-applikation-app-template/lizenz-manager/

# Nuget
Get the latest version from nuget.org<br>
[![NuGet](https://img.shields.io/nuget/v/SharedMauiCoreLibrary.Licensing.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SharedMauiCoreLibrary.Licensing/)
[![NuGet](https://img.shields.io/nuget/dt/SharedMauiCoreLibrary.Licensing.svg)](https://www.nuget.org/packages/SharedMauiCoreLibrary.Licensing)

## Available content
Please find a list of available content below.

### Usage

#### Namespace
```xaml
xmlns:behaviors="clr-namespace:AndreasReitberger.Shared.Core.Licensing;assembly=SharedMauiCoreLibrary.Licensing"
```

```cs
using AndreasReitberger.Shared.Core.Licensing
```

#### LicenseManager
In order to use `LicenseManager`, create a new `Instance` like shown below. .

```cs
string licenseUri = "andreas-reitberger.de";
LicenseManager manager;

//....

manager = new LicenseManager.LicenseManagerConnectionBuilder()
            .WithLicenseServer(serverAddress: licenseUri, port: null, https: true)
            .Build();

```  

For the `licenseUri` use the base WordPress store address without `https:\\` (like shown above).

#### LicenseInfo
The next step is to create a `ILicenseInfo` with the details of your product created in your WooCommerece store.


```cs

    info = new LicenseInfo.LicenseInfoBuilder()
        .WithLicense("The license key you want to check")
        .WithOptions(new LicenseOptions()
        {
            ProductName = "Name of your product",
            ProductIdentifier = "Your unique ProductId",
            LicenseCheckPattern = "^AR-((\\w{8})-){2}(\\w{8})$",
        })
        .Build();
```

#### Endpoints
If all is setup, you can perform following methods depending on your needs. All will return an `ILicenseQueryResult` object.

```cs
    ILicenseQueryResult result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
    Assert.IsTrue(result?.Success == true);

    result = await manager.DeactivateLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
    Assert.IsTrue(result?.Success == true);

    result = await manager.CheckLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
    Assert.IsTrue(result?.Success == false);

    result = await manager.ActivateLicenseAsync(license: info, LicenseServerTarget.WooCommerce);
    Assert.IsTrue(result?.Success == true);
```


