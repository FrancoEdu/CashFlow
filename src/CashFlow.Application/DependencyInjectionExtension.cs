using CashFlow.Application.Mapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Report.Excel;
using CashFlow.Application.UseCases.Report.PDF;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddMapping(services);
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
    
    private static void AddUseCases(IServiceCollection services)
    {
        #region Expense useCases
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpensesUseCase, UpdateExpenseUseCase>();
        #endregion Expense useCases

        #region Report useCases
        services.AddScoped<IGenerateExpenseReportExcelUseCase, GenerateExpenseReportExcelUseCase>();
        services.AddScoped<IGenerateExpenseReportPdfUseCase, GenerateExpenseReportPdfUseCase>();
        #endregion Report useCases
    }
}
