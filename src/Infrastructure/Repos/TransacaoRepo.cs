using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class TransacaoRepo : ITransacaoRepository
{
    private readonly DatabaseContext _context;

    public TransacaoRepo(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _context.Transacoes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> GetByPessoaIdAsync(Guid pessoaId)
    {
        return await _context.Transacoes
            .AsNoTracking()
            .Where(t => t.PessoaId == pessoaId)
            .ToListAsync();
    }

    public async Task DeleteByPessoaIdAsync(Guid pessoaId)
    {
        var transacoes = await _context.Transacoes
            .Where(t => t.PessoaId == pessoaId)
            .ToListAsync();

        if (transacoes.Count == 0)
            return;

        _context.Transacoes.RemoveRange(transacoes);
        await _context.SaveChangesAsync();
    }
}
