using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    #region Private Methods

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpenseReadOnlyRepository, ExpenseRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpenseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CashFlowDbContext>(options => 
            options.UseMySql(
                configuration.GetConnectionString("AppConnection"), 
                new MySqlServerVersion(new Version(8, 0, 34))
                )
            );
    }

    #endregion Private Methods
}
