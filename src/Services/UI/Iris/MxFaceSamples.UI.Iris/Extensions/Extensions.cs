using Microsoft.EntityFrameworkCore;
using MxFaceSamples.BuildingBlocks.Iris.Services;
using MxFaceSamples.UI.Iris.Extensions;
using MxFaceSamples.UI.Iris.Infrastructure.Data.Contexts;


public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            System.Diagnostics.Debug.WriteLine("Iris::ApplicationDbContext::conn ->" + connectionString);

            options.UseSqlite(connectionString, sqliteOptionsAction: options =>
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
            });
        });

        //builder.Services.AddScoped<IDeviceService, DeviceService>();

        builder.Services.AddHttpClient<DeviceService>(o => o.BaseAddress = new(Configuration["MxFace:ClientServiceUrl"]));


        builder.Services.AddHttpClient<IrisCapturingService>(o => o.BaseAddress = new(Configuration["MxFace:ClientServiceUrl"]));

        builder.Services.AddHttpClient<IrisMatchingService>(o => o.BaseAddress = new(Configuration["MxFace:MxFaceApiEndpointUrl"]))
            .AddSubscriptionKey();

    }
}
