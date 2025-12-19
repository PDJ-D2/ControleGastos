using Domain.Enums;

namespace API.DTOs.Categoria;

public class CriarCategoriaRequest
{
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; }
}
