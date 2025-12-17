using Domain.Entities;

namespace Application.Interfaces;

// Contrato para persistência de categorias.

public interface ICategoriaRepository
{
    Task AddAsync(Categoria categoria);
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(Guid id);
}
