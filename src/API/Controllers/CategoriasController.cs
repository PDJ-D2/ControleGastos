using Application.Services;
using Application.Interfaces;
using API.DTOs.Categoria;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("categorias")]
public class CategoriasController : ControllerBase
{
    private readonly CriarCategoriaService _criarCategoria;
    private readonly ICategoriaRepository _categoriaRepo;

    public CategoriasController(
        CriarCategoriaService criarCategoria,
        ICategoriaRepository categoriaRepo)
    {
        _criarCategoria = criarCategoria;
        _categoriaRepo = categoriaRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarCategoriaRequest request)
    {
        var id = await _criarCategoria.ExecutarAsync(
            request.Descricao,
            request.Finalidade);

        return CreatedAtAction(nameof(Listar), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> Listar(
    [FromServices] ListarCategoriasService service)
    {
        var categorias = await service.ExecutarAsync();

        return Ok(categorias.Select(c => new
        {
            c.Id,
            c.Descricao,
            c.Finalidade
        }));
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var categoria = await _categoriaRepo.GetByIdAsync(id);
        if (categoria == null)
            return NotFound();

        await _categoriaRepo.DeleteAsync(id);
        return NoContent();
    }
}
