using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.ResourcesMessages;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Report.Excel;
public class GenerateExpenseReportExcelUseCase : IGenerateExpenseReportExcelUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;

    private const string CURRENCY_SYMBOL = "R$ ";
    private const string CURRENCY_FORMAT_CELL = $"- {CURRENCY_SYMBOL} #,##0.00";
    private const string AUTHOR_NAME = "franco's Software LTDA"; 
    private const int FONT_SIZE = 12;
    private const string FONT_STYLE = "Times New Roman";
    private const string FONT_COLOR = "#F5C2B6";
    private const int START_LINE_INFOS = 2;

    public GenerateExpenseReportExcelUseCase(IExpenseReadOnlyRepository expenseReadOnlyRepository)
    {
        _expenseReadOnlyRepository = expenseReadOnlyRepository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expenseReadOnlyRepository.GetAllExpensesByMonth(month);
        if(!expenses.Any()) { return []; }

        var workbook = new XLWorkbook();

        workbook.Author = AUTHOR_NAME;
        workbook.Style.Font.FontSize = FONT_SIZE;
        workbook.Style.Font.FontName = FONT_STYLE;

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsertHeader(worksheet);
        InsertBodyInfo(worksheet, expenses);

        var file = new MemoryStream();
        workbook.SaveAs(file);
        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerateMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerateMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerateMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerateMessages.DESCRIPTION;
        worksheet.Cell("E1").Value = ResourceReportGenerateMessages.AMOUNT;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml(FONT_COLOR);
        worksheet.Cells("A1:D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }

    private void InsertBodyInfo(IXLWorksheet worksheet, List<Expense> expenses)
    {
        var aux = START_LINE_INFOS;
        foreach (var item in expenses)
        {
            worksheet.Cell($"A{aux}").Value = item.Title;
            worksheet.Cell($"B{aux}").Value = item.Date;
            worksheet.Cell($"C{aux}").Value = ConvertPaymentTypeToString(item.PaymentType);
            worksheet.Cell($"D{aux}").Value = item.Description;
            worksheet.Cell($"E{aux}").Value = item.Amount;
            worksheet.Cell($"E{aux}").Style.NumberFormat.Format = CURRENCY_FORMAT_CELL;
            aux++;
        }
    }

    private string ConvertPaymentTypeToString(PaymentType paymentType)
    {
        switch (paymentType)
        {
            case PaymentType.Cash:
                return ResourceReportGenerateMessages.CASH;
            case PaymentType.CreditCard:
                return ResourceReportGenerateMessages.CREDIT_CARD;
            case PaymentType.DebitCard:
                return ResourceReportGenerateMessages.DEBIT_CARD;
            case PaymentType.EletronicTransfer:
                return ResourceReportGenerateMessages.ELETRONIC_TRANSFER;
            default:
                return ResourceReportGenerateMessages.PAYMENT_TYPE_NOT_FOUND;
        }
    }
}
