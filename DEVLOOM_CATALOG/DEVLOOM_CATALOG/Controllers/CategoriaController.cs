using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Application.DTOs;
using DEVLOOM_CATALOG.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DEVLOOM_CATALOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCategoriaAsync([FromBody] CategoriaRequestDto categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            var resultado = await _categoriaService.AdicionaAsync(categoria);
            if (!resultado) return BadRequest("Erro ao adicionar categoria.");

            var categoriaResponse = _mapper.Map<CategoriaResponseDto>(categoria);
            return Ok(categoriaResponse);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaTodasCategoriasAsync()
        {
            var categorias = await _categoriaService.RecuperaTodasAsync();
            var categoriasDto = categorias.Select(c => _mapper.Map<CategoriaResponseDto>(c));
            return Ok(categoriasDto);
        }
    }
}
