using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NarutoDatabookApp.Data;

namespace NarutoDatabookApp
{
    public static class Seeder
    {
        public static void Run(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seed = services.GetRequiredService<Seed>();
                var logger = services.GetRequiredService<ILogger<Seed>>();

                try
                {
                    seed.SeedDataContext();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}
