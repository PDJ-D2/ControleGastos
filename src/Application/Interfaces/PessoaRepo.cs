using Domain.Entities;

namespace Application.Interfaces;

// Contrato para persistência de pessoas.

public interface IPessoaRepository
{
    Task AddAsync(Pessoa pessoa);
    Task<IEnumerable<Pessoa>> GetAllAsync();
    Task<Pessoa?> GetByIdAsync(Guid id);

    // Remove uma pessoa.
    // Todas as transações associadas devem ser removidas em cascata.
 
    Task DeleteAsync(Guid id);
}
