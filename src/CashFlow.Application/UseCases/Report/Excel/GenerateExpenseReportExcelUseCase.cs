using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Report.Excel;
public class GenerateExpenseReportExcelUseCase : IGenerateExpenseReportExcelUseCase
{
    private const string _authorName = "franco's Software LTDA"; 
    private const int _fontSize = 12;
    private const string _fontStyle = "Times New Roman";

    public async Task<byte[]> Execute(DateOnly month)
    {
        var workbook = new XLWorkbook();

        workbook.Author = _authorName;
        workbook.Style.Font.FontSize = _fontSize;
        workbook.Style.Font.FontName = _fontStyle;

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
    }
}
