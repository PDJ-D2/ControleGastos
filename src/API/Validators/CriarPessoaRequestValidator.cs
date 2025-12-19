using API.DTOs.Pessoa;
using FluentValidation;

namespace API.Validators.Pessoa;

public class CriarPessoaRequestValidator : AbstractValidator<CriarPessoaRequest>
{
    public CriarPessoaRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Idade)
            .GreaterThan(0)
            .LessThan(120);
    }
}
