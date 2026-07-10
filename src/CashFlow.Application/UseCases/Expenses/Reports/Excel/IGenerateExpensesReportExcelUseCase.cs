namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<Byte[]> Execute(DateOnly month);
}