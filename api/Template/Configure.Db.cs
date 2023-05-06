using ServiceStack.Data;
using ServiceStack.OrmLite;
using Template.ServiceModel;

[assembly: HostingStartup(typeof(Template.ConfigureDb))]

namespace Template;

// Database can be created with "dotnet run --AppTasks=migrate"   
public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context,services) => services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
            context.Configuration.GetConnectionString("DefaultConnection") ?? "App_Data/db.sqlite",
            SqliteDialect.Provider)))
        // Create non-existing Table and add Seed Data Example
        .ConfigureAppHost(appHost => {
            using var db = appHost.Resolve<IDbConnectionFactory>().Open();
            if (db.CreateTableIfNotExists<FormGroup>())
            {
                // Seed data
                db.Insert(new FormGroup
                {
                    Name = "Init Form 1",
                    Index = 1
                });
            }
        });
}