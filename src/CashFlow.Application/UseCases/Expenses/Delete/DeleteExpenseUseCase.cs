using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception.Resources;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(
    IExpensesWriteOnlyRepository repository,
    IUnitOfWork unitOfWork
    ) : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id) ;
        if(!result) throw new NotFoundException(ResourcesErrorMessages.EXPENSE_NOT_FOUND);
        await _unitOfWork.Commit();
    }
}