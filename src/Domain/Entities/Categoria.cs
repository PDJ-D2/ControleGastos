using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

// Representa uma categoria de transação

public class Categoria
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public FinalidadeCategoria Finalidade { get; private set; }

    protected Categoria() { }

    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da categoria é obrigatória.");

        Id = Guid.NewGuid();
        Descricao = descricao;
        Finalidade = finalidade;
    }

    // Verifica se a categoria pode ser usada para o tipo de transação informado
    public bool PermiteTipo(TipoTransacao tipo)
    {
        return Finalidade == FinalidadeCategoria.Ambas ||
               (Finalidade == FinalidadeCategoria.Despesa && tipo == TipoTransacao.Despesa) ||
               (Finalidade == FinalidadeCategoria.Receita && tipo == TipoTransacao.Receita);
    }
}
