namespace CashFlow.Application.UseCases.Report.PDF;
public interface IGenerateExpenseReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
