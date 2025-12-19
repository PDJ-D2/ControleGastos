using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

// Serviço responsável por listar todas as transações cadastradas.

public class ListarTransacoesService
{
    private readonly ITransacaoRepository _transacaoRepo;

    public ListarTransacoesService(ITransacaoRepository transacaoRepo)
    {
        _transacaoRepo = transacaoRepo;
    }

    public Task<IEnumerable<Transacao>> ExecutarAsync()
    {
        return _transacaoRepo.GetAllAsync();
    }
}
