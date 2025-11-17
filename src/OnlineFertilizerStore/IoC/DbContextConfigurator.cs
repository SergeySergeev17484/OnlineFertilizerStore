using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using OnlineFertilizerStore.Settings;

namespace OnlineFertilizerStore.IoC
{
    public static class DbContextConfigurator
    {
        public static void ConfigureService(IServiceCollection services, OnlineFertilizerStoreSettings settings)
        {
            services.AddDbContextFactory<OnlineFertilizerStoreDbContext>(options =>
            {
                options.UseNpgsql(settings.OnlineFertilizerStoreDbContext);
            }, ServiceLifetime.Scoped);

        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<OnlineFertilizerStoreDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate();
        }
    }
}
