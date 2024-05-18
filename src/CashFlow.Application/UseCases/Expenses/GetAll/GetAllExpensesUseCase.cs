using AutoMapper;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpenseReadOnlyRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<GetAllExponsesResponseJson> Execute()
    {
        var expenses = _mapper.Map<List<ShortExpenseResponseJson>>(await _expenseRepository.GetAll());
        return new GetAllExponsesResponseJson
        {
            Expense = expenses
        };
    }
}
