namespace CashFlow.Exception.ExceptionBase;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string errorMessage) : base(errorMessage)
    { 
    }
}
