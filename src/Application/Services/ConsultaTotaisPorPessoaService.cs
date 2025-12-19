using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class ConsultaTotaisPorPessoaService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public ConsultaTotaisPorPessoaService(
        IPessoaRepository pessoaRepo,
        ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task<(IEnumerable<Pessoa> Pessoas, decimal TotalReceitas, decimal TotalDespesas)> ExecutarAsync()
    {
        var pessoas = await _pessoaRepo.GetAllAsync();
        var transacoes = await _transacaoRepo.GetAllAsync();

        decimal totalReceitas = 0;
        decimal totalDespesas = 0;

        foreach (var pessoa in pessoas)
        {
            var transacoesPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id);

            totalReceitas += transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            totalDespesas += transacoesPessoa
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);
        }

        return (pessoas, totalReceitas, totalDespesas);
    }
}
