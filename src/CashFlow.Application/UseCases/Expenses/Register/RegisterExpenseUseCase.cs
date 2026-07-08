using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Application.UseCases.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository = repository; 
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {

        Validate(request);   

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterExpenseJson>(entity);
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
