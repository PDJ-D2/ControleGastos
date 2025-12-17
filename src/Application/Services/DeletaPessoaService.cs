using Application.Repos;

namespace Application.Services;

// Caso de uso responsável por remover uma pessoa e suas transações.

public class DeletarPessoaService
{
    private readonly PessoaRepo _pessoaRepo;
    private readonly TransacaoRepo _transacaoRepo;

    public DeletaPessoaService(
        PessoaRepo pessoaRepo,
        TransacaoRepo transacaoRepo)
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
