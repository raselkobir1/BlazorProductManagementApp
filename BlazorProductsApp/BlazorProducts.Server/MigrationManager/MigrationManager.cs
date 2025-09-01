using BlazorProducts.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorProducts.Server.MigrationManager
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Migration failed: {ex.Message}");
                        throw;
                    }
                }
            }

            return host;
        }
    }
}
