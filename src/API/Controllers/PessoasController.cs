using API.DTOs.Pessoa;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("pessoas")]
public class PessoasController : ControllerBase
{
    private readonly CriarPessoaService _criarPessoa;
    private readonly DeletarPessoaService _deletarPessoa;
    private readonly IPessoaRepository _pessoaRepo;

    public PessoasController(
        CriarPessoaService criarPessoa,
        DeletarPessoaService deletarPessoa,
        IPessoaRepository pessoaRepo)
    {
        _criarPessoa = criarPessoa;
        _deletarPessoa = deletarPessoa;
        _pessoaRepo = pessoaRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request)
    {
        var id = await _criarPessoa.ExecutarAsync(request.Nome, request.Idade);
        return CreatedAtAction(nameof(Listar), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var pessoas = await _pessoaRepo.GetAllAsync();
        return Ok(pessoas.Select(p => new
        {
            p.Id,
            p.Nome,
            p.Idade
        }));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _deletarPessoa.ExecutarAsync(id);
        return NoContent();
    }

    [HttpGet("totais")]
    public async Task<IActionResult> Totais(
    [FromServices] ConsultaTotaisPorPessoaService service,
    [FromServices] ITransacaoRepository transacaoRepo)
    {
        var (pessoas, totalReceitas, totalDespesas) = await service.ExecutarAsync();
        var transacoes = await transacaoRepo.GetAllAsync();

        var response = pessoas.Select(p =>
        {
            var transacoesPessoa = transacoes.Where(t => t.PessoaId == p.Id);

            var receitas = transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var despesas = transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            return new
            {
                p.Id,
                p.Nome,
                TotalReceitas = receitas,
                TotalDespesas = despesas,
                Saldo = receitas - despesas
            };
        });

        return Ok(new
        {
            Pessoas = response,
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            Saldo = totalReceitas - totalDespesas
        });
    }
}
