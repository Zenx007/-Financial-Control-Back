using FamilyFinancialControl.Core.RepositoriesInterface;
using FamilyFinancialControl.Infrastructure.RepositoriesImpl;

namespace FamilyFinancialControl.API.Extensions;

public static class AddRepositoriesProgram
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}