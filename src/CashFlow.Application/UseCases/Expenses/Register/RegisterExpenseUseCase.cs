using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository; 

    public RegisterExpenseUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }

    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {

        Validate(request);   

        var entity = new Expense
        {
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            Title = request.Title,
            Paymenttype = (Domain.Enums.PaymentType)request.PaymentType,
        };

        _repository.Add(entity);
        
        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var result = new RegisterExpenseValidator().Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
