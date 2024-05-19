using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class NotFoundException : CashFlowException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors()
    {
        return new List<string>() { Message };
    }

    public NotFoundException(string errorMessage) : base(errorMessage){ }
}
