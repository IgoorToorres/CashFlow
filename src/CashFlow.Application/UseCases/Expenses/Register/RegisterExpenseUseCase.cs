using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {

        Validate(request);        
        
        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var isEmptyTitle = string.IsNullOrWhiteSpace(request.Title);
        if (isEmptyTitle)
        {
            throw new ArgumentException("Title is Required.");
        }

        if(request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        var isFutureDate = DateTime.Compare(request.Date, DateTime.UtcNow);
        if(isFutureDate > 0)
        {
            throw new ArgumentException("Expenses cannot be for the future");
        }

        var isValidPaymentType = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (!isValidPaymentType)
        {
            throw new ArgumentException("Payment Type is not valid");
        }      
    }
}
