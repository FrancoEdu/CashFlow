using AutoMapper;
using CashFlow.Communication.Requests.Expense;
using CashFlow.Communication.Response.Expense;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<ExpenseRegisterRequestJson, Expense>();
        CreateMap<ExpenseUpdateRequestJson, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ShortExpenseResponseJson>();
        CreateMap<Expense, ExpenseResponseJson>();
    }
}
