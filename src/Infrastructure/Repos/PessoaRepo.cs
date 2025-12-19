using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repos;

public class PessoaRepo : IPessoaRepository
{
    private readonly DatabaseContext _context;

    public PessoaRepo(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Pessoa>> GetAllAsync()
    {
        return await _context.Pessoas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Pessoa?> GetByIdAsync(Guid id)
    {
        return await _context.Pessoas.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null) return;

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
    }
}
