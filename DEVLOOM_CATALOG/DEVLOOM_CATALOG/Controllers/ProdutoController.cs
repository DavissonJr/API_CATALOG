using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Application.DTOs;
using DEVLOOM_CATALOG.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DEVLOOM_CATALOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProdutoAsync([FromBody] ProdutoRequestDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            var resultado = await _produtoService.AdicionaAsync(produto);
            if (!resultado) return BadRequest("Erro ao adicionar produto.");

            var produtoResponse = _mapper.Map<ProdutoResponseDto>(produto);
            return Ok(produtoResponse);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarProdutosPorCategoriaAsync(
            [FromQuery] Guid categoriaId,
            [FromQuery] int page,
            [FromQuery] int size)
        {
            if (page <= 0 || size <= 0)
                return BadRequest("Página e tamanho devem ser maiores que zero.");

            var produtosPaginados = await _produtoService.BuscarPorCategoriaPaginadoAsync(categoriaId, page, size);

            var produtosDto = produtosPaginados.Retorno
                .Select(p => _mapper.Map<ProdutoResponseDto>(p))
                .ToList();

            var retorno = new
            {
                produtosPaginados.TotalRegistros,
                produtosPaginados.Pagina,
                produtosPaginados.QtdPagina,
                Retorno = produtosDto
            };

            return Ok(retorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaProdutoPorIdAsync(Guid id)
        {
            var produto = await _produtoService.BuscaPorIdAsync(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            var produtoDto = _mapper.Map<ProdutoResponseDto>(produto);
            return Ok(produtoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProdutoAsync(Guid id, [FromBody] ProdutoRequestDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            produto.Id = id;

            var resultado = await _produtoService.AtualizarAsync(produto);
            if (!resultado) return NotFound("Erro ao atualizar produto.");

            var produtoResponse = _mapper.Map<ProdutoResponseDto>(produto);
            return Ok(produtoResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProdutoAsync(Guid id)
        {
            var resultado = await _produtoService.DeletarAsync(id);
            if (!resultado) return NotFound("Produto não encontrado.");

            return NoContent();
        }
    }
}
