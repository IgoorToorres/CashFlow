using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class NotFoundException(string message) : CashFlowException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}