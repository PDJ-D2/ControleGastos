using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repos;

// Implementação em memória do repositório de categorias.

public class CategoriaRepoInMemory : ICategoriaRepository
{
    private static readonly List<Categoria> _categorias = new();

    public Task AddAsync(Categoria categoria)
    {
        _categorias.Add(categoria);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Categoria>> GetAllAsync()
        => Task.FromResult(_categorias.AsEnumerable());

    public Task<Categoria?> GetByIdAsync(Guid id)
        => Task.FromResult(_categorias.FirstOrDefault(c => c.Id == id));
}
