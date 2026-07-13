using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Resources;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository) : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENT_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository = repository;

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if(expenses.Count == 0) return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        var paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font {Name = FontHelper.RALEWAY_REGULAR, Size = 15});
        paragraph.AddLineBreak();

        var totalExpenses = expenses.Sum(expense => expense.Amount);
        paragraph.AddFormattedText($"{CURRENT_SYMBOL} {totalExpenses}", new Font {Name = FontHelper.WORKSANS_BLACK, Size = 50});

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y}";
        document.Info.Author = "Igor";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.BottomMargin = 80;
        section.PageSetup.TopMargin = 80;

        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var render = new PdfDocumentRenderer
        {
            Document = document,
        };

        render.RenderDocument();
        using var file = new MemoryStream();
        render.PdfDocument.Save(file);
        
        return file.ToArray();
    }
}