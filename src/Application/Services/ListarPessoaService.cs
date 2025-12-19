using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

// Serviço responsável por listar todas as pessoas cadastradas.
 
public class ListarPessoasService
{
    private readonly IPessoaRepository _pessoaRepo;

    public ListarPessoasService(IPessoaRepository pessoaRepo)
    {
        _pessoaRepo = pessoaRepo;
    }

    public Task<IEnumerable<Pessoa>> ExecutarAsync()
    {
        return _pessoaRepo.GetAllAsync();
    }
}
