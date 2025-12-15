using Domain.Exceptions;

namespace Domain.Entities;

// Representa uma pessoa do sistema

public class Pessoa
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int Idade { get; private set; }

    protected Pessoa() { }

    public Pessoa(string nome, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome da pessoa é obrigatório.");

        if (idade <= 0)
            throw new DomainException("A idade deve ser um número positivo.");

        Id = Guid.NewGuid();
        Nome = nome;
        Idade = idade;
    }

    public bool MenorDeIdade()
        => Idade < 18;
}
