using AutoMapper;
using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;
using PdfSharp.Fonts;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
       AddPdfFontResolver();
       AddAutoMapper(services);
       AddUseCases(services);
    }

    private static void AddPdfFontResolver()
    {
        GlobalFontSettings.FontResolver ??= new ExpensesReportFontResolver();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(this IServiceCollection services)
    {
         services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
         services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
         services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
         services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
         services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
         services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
         services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase >();
         services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase >();
         services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase >();
    }
}
