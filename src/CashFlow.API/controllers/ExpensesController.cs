using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RequestRegisterExpenseJson request, [FromServices] IRegisterExpenseUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}