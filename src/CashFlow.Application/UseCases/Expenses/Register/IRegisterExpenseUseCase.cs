using CashFlow.Communication.Requests;
using CashFlow.Communication.Response.Expense;

namespace CashFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(ExpenseRegisterRequestJson request);
}
