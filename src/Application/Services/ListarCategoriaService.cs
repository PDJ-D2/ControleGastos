using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

// Serviço responsável por listar todas as categorias cadastradas.

public class ListarCategoriasService
{
    private readonly ICategoriaRepository _categoriaRepo;

    public ListarCategoriasService(ICategoriaRepository categoriaRepo)
    {
        _categoriaRepo = categoriaRepo;
    }

    public Task<IEnumerable<Categoria>> ExecutarAsync()
    {
        return _categoriaRepo.GetAllAsync();
    }
}
