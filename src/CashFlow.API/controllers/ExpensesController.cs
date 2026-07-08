using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RequestRegisterExpenseJson request, [FromServices] IRegisterExpenseUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }


    
}