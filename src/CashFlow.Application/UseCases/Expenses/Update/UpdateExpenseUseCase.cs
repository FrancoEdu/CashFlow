using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpensesUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpenseUpdateOnlyRepository _expenseUpdateOnlyRepository;
    private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;

    public UpdateExpenseUseCase(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IExpenseUpdateOnlyRepository expenseUpdateOnlyRepository, 
        IExpenseReadOnlyRepository expenseReadOnlyRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _expenseUpdateOnlyRepository = expenseUpdateOnlyRepository;
        _expenseReadOnlyRepository = expenseReadOnlyRepository;
    }

    public async Task Execute(long id, ExpenseUpdateRequestJson request)
    {
        Validate(request);
        var existisExpense = await _expenseReadOnlyRepository.GetByIdWithOutAsNoTracking(id);
        if (existisExpense is null) { throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND); }

        _mapper.Map(request, existisExpense);
        _expenseUpdateOnlyRepository.Update(existisExpense);
        await _unitOfWork.Commit();
    }

    #region Private Methods
    private void Validate(ExpenseUpdateRequestJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
    #endregion Private Methods
}
