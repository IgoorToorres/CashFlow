public interface IGenerateExpensesReportPdfUseCase
{
    Task<Byte[]> Execute(DateOnly month);
}