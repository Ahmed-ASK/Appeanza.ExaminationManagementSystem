using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;

namespace Appeanza.ExaminationManagementSystem.APIs.Extentions
{
    public static class Initializer
    {

        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app) 
        {
            using var scope = app.Services.CreateAsyncScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            var ExaminationDbInitializer = service.GetRequiredService<IExaminationDbInitializer>();
            var ExaminationIdentityDbInitializer = service.GetRequiredService<IExaminationIdentityDbInitializer>();

            try
            {
                await ExaminationDbInitializer.InitializeAsync();
                //await ExaminationDbInitializer.SeedAsync(); // Not Implemented Yet
                await ExaminationIdentityDbInitializer.InitializeAsync();
                await ExaminationIdentityDbInitializer.SeedAsync();
            }
            catch (Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying the migrations or the data seeding");
            }
            return app;
        }

    }
}
