using Domain.Entities;

namespace Application.Interfaces;

// Contrato para persistência de transações.

public interface ITransacaoRepository
{
    Task AddAsync(Transacao transacao);
    Task<IEnumerable<Transacao>> GetAllAsync();

    // Retorna todas as transações de uma pessoa.

    Task<IEnumerable<Transacao>> GetByPessoaIdAsync(Guid pessoaId);

    // Remove todas as transações associadas a uma pessoa.

    Task DeleteByPessoaIdAsync(Guid pessoaId);
}
