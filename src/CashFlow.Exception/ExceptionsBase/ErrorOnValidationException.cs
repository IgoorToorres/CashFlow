using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Empty)
{

    private readonly List<string> _erros = errorMessages;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
       return _erros;
    }
}