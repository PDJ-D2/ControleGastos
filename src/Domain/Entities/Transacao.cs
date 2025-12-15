using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

// Representa uma transação financeira

public class Transacao
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }

    public Guid PessoaId { get; private set; }
    public Guid CategoriaId { get; private set; }

    protected Transacao() { }

    public Transacao(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        Pessoa pessoa,
        Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da transação é obrigatória.");

        if (valor <= 0)
            throw new DomainException("O valor da transação deve ser positivo.");

        // menor de idade não pode ter receita
        if (pessoa.MenorDeIdade() && tipo == TipoTransacao.Receita)
            throw new DomainException("Pessoa menor de idade não pode ter receita.");

        // categoria deve permitir o tipo da transação
        if (!categoria.PermiteTipo(tipo))
            throw new DomainException("A categoria informada não permite este tipo de transação.");

        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;

        PessoaId = pessoa.Id;
        CategoriaId = categoria.Id;
    }
}
