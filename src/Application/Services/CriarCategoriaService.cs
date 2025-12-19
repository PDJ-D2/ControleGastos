using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

// Caso de uso responsável por criar categorias.

public class CriarCategoriaService
{
    private readonly ICategoriaRepository _categoriaRepo;

    public CriarCategoriaService(ICategoriaRepository categoriaRepo)
    {
        _categoriaRepo = categoriaRepo;
    }

    public async Task<Guid> ExecutarAsync(string descricao, FinalidadeCategoria finalidade)
    {
        var categoria = new Categoria(descricao, finalidade);

        await _categoriaRepo.AddAsync(categoria);

        return categoria.Id;
    }
}
