using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception.Resources;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{

    private readonly IExpensesRepository _repository;
    private readonly IMapper _mapper;


    public GetExpenseByIdUseCase(IExpensesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id) ?? throw new NotFoundException(ResourcesErrorMessages.EXPENSE_NOT_FOUND);
        
        return _mapper.Map<ResponseExpenseJson>(result);
    }
}