using AfinApp.Worker;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AfinAppContext>(Const.db, configureDbContextOptions: options =>
{
    // options.UseSeeding((context, _) =>
    // {
    //     if (!context.Set<MaintanceSpot>().Any())
    //     {
    //         SeedSpots(context);
    //         context.SaveChanges();
    //     }
    // })
    // .UseAsyncSeeding(async (context, _, cancellationToken) =>
    // {
    //     if (!context.Set<MaintanceSpot>().Any())
    //     {
    //         SeedSpots(context);
    //         await context.SaveChangesAsync(cancellationToken);
    //     }
    // });
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();