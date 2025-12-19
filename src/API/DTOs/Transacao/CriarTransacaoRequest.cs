using Domain.Enums;

namespace API.DTOs.Transacao;

public class CriarTransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid PessoaId { get; set; }
    public Guid CategoriaId { get; set; }
}
