
using CashFlow.Application.UseCases.Report.PDF.Colors;
using CashFlow.Application.UseCases.Report.PDF.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.ResourcesMessages;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Report.PDF;
public class GenerateExpenseReportPdfUseCase : IGenerateExpenseReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    private readonly IExpenseReadOnlyRepository _expenseReadOnlyExpense;

    public GenerateExpenseReportPdfUseCase(IExpenseReadOnlyRepository expenseReadOnlyExpense)
    {
        _expenseReadOnlyExpense = expenseReadOnlyExpense;

        // ensina a biblioteca a como resolver as fontes
        GlobalFontSettings.FontResolver = new ExpenseReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expenseReadOnlyExpense.GetAllExpensesByMonth(month);
        if(!expenses.Any()) { return []; }

        var document = CreateDocument(month);
        var page = CreatePage(document);
        MountHeader(page, expenses, month);

        expenses.ForEach(expense =>
        {
            var table = CreateExpenseTable(page);
            MountTableData(table, expense);
        });

        return RenderDocument(document);
    }

    #region Private Methods
    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerateMessages.EXPENSES_FOR} {month:Y}";
        document.Info.Author = ResourceReportGenerateMessages.AUTHOR_NAME;

        var styles = document.Styles["Normal"];
        styles!.Font.Name = FontHelp.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();
        
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private byte[] RenderDocument(Document document)
    {
        var render = new PdfDocumentRenderer
        {
            Document = document,
        };

        render.RenderDocument();

        using var file = new MemoryStream();
        render.PdfDocument.Save(file);

        return file.ToArray();
    }
    
    private void MountHeader(Section page, List<Expense> expenses, DateOnly mounth)
    {
        var table = page.AddTable();
        //table.AddColumn("64");
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);

        //row.Cells[0].AddImage(Path.Combine(directoryName!, "UseCases\\Report\\PDF\\Logo", "study.png"));
        row.Cells[0].AddParagraph("Hey, Eduardo Franco");
        row.Cells[0].Format.Font = new Font { Name = FontHelp.RALEWAY_BOLD, Size = 16 };
        row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

        var paragraph = page.AddParagraph();

        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerateMessages.TOTAL_SPENT_IN, mounth.ToString("Y"));
        paragraph.AddFormattedText(title, new Font { Name = FontHelp.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();

        var spent = expenses.Sum(x => x.Amount);
        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {spent}", new Font { Name = FontHelp.RALEWAY_BOLD, Size = 26 });
    }

    private void MountTableData(Table table, Expense expense)
    {
        var row = table.AddRow();
        row.Height = HEIGHT_ROW_EXPENSE_TABLE;

        AddExpenseTitle(row.Cells[0], expense.Title);
        AddHeaderForAmount(row.Cells[3]);

        row = table.AddRow();
        row.Height = HEIGHT_ROW_EXPENSE_TABLE;

        row.Cells[0].AddParagraph(expense.Date.ToString("D"));
        SetStyleBaseForExpenseInformation(row.Cells[0]);
        row.Cells[0].Format.LeftIndent = 20;

        row.Cells[1].AddParagraph(expense.Date.ToString("t"));
        SetStyleBaseForExpenseInformation(row.Cells[1]);

        row.Cells[2].AddParagraph(expense.PaymentType.PaymentTypeToString());
        SetStyleBaseForExpenseInformation(row.Cells[2]);

        AddAmountForExpense(row.Cells[3], expense.Amount);

        if (!string.IsNullOrWhiteSpace(expense.Description))
        {
            var descriptionRow = table.AddRow();
            descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

            descriptionRow.Cells[0].AddParagraph(expense.Description);
            descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelp.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
            descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
            descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            descriptionRow.Cells[0].MergeRight = 2;
            descriptionRow.Cells[0].Format.LeftIndent = 20;

            row.Cells[3].MergeDown = 1;
        }

        AddWhiteSpace(table);
    }

    private void AddExpenseTitle(Cell cell, string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.Format.Font = new Font { Name = FontHelp.RALEWAY_BOLD, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerateMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelp.RALEWAY_BOLD, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelp.WORKSANS_BOLD, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForExpense(Cell cell, decimal amount)
    {
        cell.AddParagraph($"-{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelp.WORKSANS_BOLD, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    #endregion Private Methods
}
