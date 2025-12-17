using Application.Repos;
using Domain.Entities;

namespace Infrastructure.Repos;


// Implementação em memória do repositório de transações.

public class TransacaoRepoInMemory : TransacaoRepo
{
    private static readonly List<Transacao> _transacoes = new();

    public Task AddAsync(Transacao transacao)
    {
        _transacoes.Add(transacao);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Transacao>> GetAllAsync()
        => Task.FromResult(_transacoes.AsEnumerable());

    public Task<IEnumerable<Transacao>> GetByPessoaIdAsync(Guid pessoaId)
        => Task.FromResult(
            _transacoes.Where(t => t.PessoaId == pessoaId).AsEnumerable()
        );

    public Task DeleteByPessoaIdAsync(Guid pessoaId)
    {
        _transacoes.RemoveAll(t => t.PessoaId == pessoaId);
        return Task.CompletedTask;
    }
}
