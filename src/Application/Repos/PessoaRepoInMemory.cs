using Application.Repos;
using Domain.Entities;

namespace Infrastructure.Repos;

// Implementação em memória do repositório de pessoas.

public class PessoaRepoInMemory : PessoaRepo
{
    private static readonly List<Pessoa> _pessoas = new();

    public Task AddAsync(Pessoa pessoa)
    {
        _pessoas.Add(pessoa);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Pessoa>> GetAllAsync()
        => Task.FromResult(_pessoas.AsEnumerable());

    public Task<Pessoa?> GetByIdAsync(Guid id)
        => Task.FromResult(_pessoas.FirstOrDefault(p => p.Id == id));

    public Task DeleteAsync(Guid id)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == id);
        if (pessoa != null)
            _pessoas.Remove(pessoa);

        return Task.CompletedTask;
    }
}
