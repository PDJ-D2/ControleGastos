using Application.Repos;
using Domain.Entities;

namespace Application.Services;

// Caso de uso responsável por criar pessoas.

public class CriarPessoaService
{
    private readonly PessoaRepo _pessoaRepo;

    public CriarPessoaService(PessoaRepo pessoaRepo)
    {
        _pessoaRepo = pessoaRepo;
    }

    public async Task<Guid> ExecutarAsync(string nome, int idade)
    {
        var pessoa = new Pessoa(nome, idade);

        await _pessoaRepo.AddAsync(pessoa);

        return pessoa.Id;
    }
}
