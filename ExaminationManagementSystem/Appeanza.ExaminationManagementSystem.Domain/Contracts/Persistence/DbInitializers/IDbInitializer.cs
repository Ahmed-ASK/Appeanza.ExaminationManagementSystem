namespace Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
