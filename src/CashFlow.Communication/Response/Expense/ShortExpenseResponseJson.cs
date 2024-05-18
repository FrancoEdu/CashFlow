namespace CashFlow.Communication.Response.Expense;
public class ShortExpenseResponseJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
