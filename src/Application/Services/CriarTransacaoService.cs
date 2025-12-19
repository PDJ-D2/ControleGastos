using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

// Caso de uso responsável por criar transações.

public class CriarTransacaoService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public CriarTransacaoService(
        IPessoaRepository pessoaRepo,
        ICategoriaRepository categoriaRepo,
        ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _categoriaRepo = categoriaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task<Guid> ExecutarAsync(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        Guid pessoaId,
        Guid categoriaId)
    {
        var pessoa = await _pessoaRepo.GetByIdAsync(pessoaId)
            ?? throw new Exception("Pessoa não encontrada.");

        var categoria = await _categoriaRepo.GetByIdAsync(categoriaId)
            ?? throw new Exception("Categoria não encontrada.");

        var transacao = new Transacao(
            descricao,
            valor,
            tipo,
            pessoa,
            categoria);

        await _transacaoRepo.AddAsync(transacao);

        return transacao.Id;
    }
}
