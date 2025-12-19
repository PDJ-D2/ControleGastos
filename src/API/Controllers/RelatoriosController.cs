using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("relatorios")]
public class RelatoriosController : ControllerBase
{
    [HttpGet("pessoas")]
    public async Task<IActionResult> TotaisPorPessoa(
        [FromServices] TotaisPorPessoaService service)
    {
        var resultado = await service.ExecutarAsync();
        return Ok(resultado);
    }
}
