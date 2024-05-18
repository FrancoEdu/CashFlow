using AutoMapper;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetByIdUseCase : IGetByIdUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetByIdUseCase(IExpenseReadOnlyRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseResponseJson> Execute(long id)
    {
        var response = _mapper.Map<ExpenseResponseJson>(await _expenseRepository.GetById(id));
        if(response == null) throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        return response;
    }
}
