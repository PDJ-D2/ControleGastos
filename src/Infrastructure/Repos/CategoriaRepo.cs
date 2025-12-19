using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class CategoriaRepo : ICategoriaRepository
{
    private readonly DatabaseContext _context;

    public CategoriaRepo(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(Guid id)
    {
        return await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
