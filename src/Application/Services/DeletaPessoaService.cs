using Application.Interfaces;

namespace Application.Services;

// Caso de uso responsável por remover uma pessoa e suas transações.

public class DeletarPessoaService
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public DeletarPessoaService(
        IPessoaRepository pessoaRepo,
        ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task ExecutarAsync(Guid pessoaId)
    {
        // Remove transações primeiro (regra de aplicação)
        await _transacaoRepo.DeleteByPessoaIdAsync(pessoaId);

        // Remove a pessoa
        await _pessoaRepo.DeleteAsync(pessoaId);
    }
}
