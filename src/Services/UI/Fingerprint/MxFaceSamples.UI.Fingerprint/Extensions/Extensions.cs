using Microsoft.EntityFrameworkCore;
using MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;
using MxFaceSamples.BuildingBlocks.Fingerprint.Services;
using MxFaceSamples.UI.Fingerprint.Extensions;
using MxFaceSamples.UI.Fingerprint.Infrastructure.Data.Contexts;
using MxFaceSamples.UI.Fingerprint.Services;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            System.Diagnostics.Debug.WriteLine("Fingerprint::ApplicationDbContext::conn ->" + connectionString);

            options.UseSqlite(connectionString, sqliteOptionsAction: options =>
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
            });
        });

        //builder.Services.AddScoped<IDeviceService, DeviceService>();

        builder.Services.AddHttpClient<DeviceService>(o => o.BaseAddress = new(Configuration["MxFace:ClientServiceUrl"]));


        builder.Services.AddHttpClient<FingerprintCapturingService>(o => o.BaseAddress = new(Configuration["MxFace:ClientServiceUrl"]));

        builder.Services.AddHttpClient<FingerprintMatchingService>(o => o.BaseAddress = new(Configuration["MxFace:MxFaceApiEndpointUrl"]))
            .AddSubscriptionKey();

    }
}
