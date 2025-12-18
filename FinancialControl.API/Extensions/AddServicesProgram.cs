using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Infrastructure.ServicesImpl;

namespace FamilyFinancialControl.API.Extensions;

public static class AddServicesProgram
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
