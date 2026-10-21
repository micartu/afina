namespace AfinApp.Worker;

using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;

public class Worker(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        AfinAppContext afinaContext = scope.ServiceProvider
            .GetRequiredService<AfinAppContext>();

        await afinaContext.Database.MigrateAsync(stoppingToken);
    }
}