using ServiceStack.Data;

[assembly: HostingStartup(typeof(MyApp.ConfigureValidation))]

namespace MyApp;

public class ConfigureValidation : IHostingStartup
{
    // Add support for dynamically generated db rules
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => services.AddSingleton<IValidationSource>(c =>
            new OrmLiteValidationSource(c.Resolve<IDbConnectionFactory>(), HostContext.LocalCache)))
        .ConfigureAppHost(appHost => {
            // Create `ValidationRule` table if it doesn't exist in AppHost.Configure() or Modular Startup
            appHost.Resolve<IValidationSource>().InitSchema();
        });
}