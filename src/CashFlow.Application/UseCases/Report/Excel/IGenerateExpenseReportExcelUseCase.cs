namespace CashFlow.Application.UseCases.Report.Excel;
public interface IGenerateExpenseReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
