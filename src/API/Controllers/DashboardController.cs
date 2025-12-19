using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public DashboardController(IPessoaRepository pessoaRepo, ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _transacaoRepo = transacaoRepo;
    }

    [HttpGet("totais")]
    public async Task<IActionResult> ObterTotais()
    {
        var pessoas = await _pessoaRepo.GetAllAsync();
        var transacoes = await _transacaoRepo.GetAllAsync();

        var lista = pessoas.Select(p =>
        {
            var receita = transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            var despesa = transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

            return new
            {
                pessoaId = p.Id,
                nome = p.Nome,
                totalReceitas = receita,
                totalDespesas = despesa,
                saldo = receita - despesa
            };
        }).ToList();

        var totaisGerais = new
        {
            totalReceitas = lista.Sum(x => x.totalReceitas),
            totalDespesas = lista.Sum(x => x.totalDespesas),
            saldo = lista.Sum(x => x.saldo)
        };

        return Ok(new { pessoas = lista, totaisGerais });
    }
}
