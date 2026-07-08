using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception.Resources;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IExpensesUpdateOnlyRepository repository
) : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository = repository;

    public async Task Execute(long id, RequestRegisterExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id) ?? throw new NotFoundException(ResourcesErrorMessages.EXPENSE_NOT_FOUND);

        _mapper.Map(request, expense);
        
        _repository.Update(expense);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}