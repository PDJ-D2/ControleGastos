using Application.Services;
using Application.Interfaces;
using API.DTOs.Transacao;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly CriarTransacaoService _criarTransacao;
    private readonly ITransacaoRepository _transacaoRepo;

    public TransacoesController(
        CriarTransacaoService criarTransacao,
        ITransacaoRepository transacaoRepo)
    {
        _criarTransacao = criarTransacao;
        _transacaoRepo = transacaoRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarTransacaoRequest request)
    {
        var id = await _criarTransacao.ExecutarAsync(
            request.Descricao,
            request.Valor,
            request.Tipo,
            request.PessoaId,
            request.CategoriaId);

        return CreatedAtAction(nameof(Listar), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> Listar(
    [FromServices] ListarTransacoesService service)
    {
        var transacoes = await service.ExecutarAsync();

        return Ok(transacoes.Select(t => new
        {
            t.Id,
            t.Descricao,
            t.Valor,
            t.Tipo,
            t.CategoriaId,
            t.PessoaId
        }));
    }
}
