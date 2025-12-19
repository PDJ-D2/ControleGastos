using Application.Interfaces;
using Domain.Enums;

namespace Application.Services;


// Calcula totais de receitas, despesas e saldo por pessoa.

public class TotaisPorPessoaService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public TotaisPorPessoaService(
        IPessoaRepository pessoaRepo,
        ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task<object> ExecutarAsync()
    {
        var pessoas = await _pessoaRepo.GetAllAsync();
        var transacoes = await _transacaoRepo.GetAllAsync();

        var resultado = pessoas.Select(p =>
        {
            var transacoesPessoa = transacoes.Where(t => t.PessoaId == p.Id);

            var totalReceitas = transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var totalDespesas = transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            return new
            {
                p.Id,
                p.Nome,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                Saldo = totalReceitas - totalDespesas
            };
        }).ToList();

        var totalGeralReceitas = resultado.Sum(r => r.TotalReceitas);
        var totalGeralDespesas = resultado.Sum(r => r.TotalDespesas);

        return new
        {
            Pessoas = resultado,
            TotalGeral = new
            {
                TotalReceitas = totalGeralReceitas,
                TotalDespesas = totalGeralDespesas,
                Saldo = totalGeralReceitas - totalGeralDespesas
            }
        };
    }
}
