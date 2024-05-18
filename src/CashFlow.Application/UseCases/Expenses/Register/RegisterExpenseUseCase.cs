using CashFlow.Domain.Entities;
using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using AutoMapper;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpenseWriteOnlyRepository _expenseWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpenseUseCase(IExpenseWriteOnlyRepository expenseWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expenseWriteRepository = expenseWriteRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(ExpenseRegisterRequestJson request)
    {
        Validate(request);

        var expense = _mapper.Map<Expense>(request);
        await _expenseWriteRepository.Add(expense);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(expense);
    }

    #region Private Methods
    private void Validate(ExpenseRegisterRequestJson request)
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
