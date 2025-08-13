using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Infra.Interfaces;
using DEVLOOM_CATALOG.Services.Interfaces;
using AutoMapper;

namespace DEVLOOM_CATALOG.Services.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<bool> AdicionaAsync(Produto produto)
        {
            return await _produtoRepository.AdicionaAsync(produto);
        }

        public async Task<bool> AtualizarAsync(Produto produto)
        {
            return await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task<Produto?> BuscaPorIdAsync(Guid id)
        {
            return await _produtoRepository.BuscaPorIdAsync(id);
        }

        public async Task<RetornoPaginado<Produto>> BuscarPorCategoriaPaginadoAsync(Guid categoriaId, int pagina, int quantidade)
        {
            return await _produtoRepository.BuscarPorCategoriaPaginadoAsync(categoriaId, pagina, quantidade);
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            return await _produtoRepository.DeletarAsync(id);
        }

        public async Task<bool> ExisteNomeNaCategoriaAsync(Guid categoriaId, string nome, Guid? ignorarId = null)
        {
            return await _produtoRepository.ExisteNomeNaCategoriaAsync(categoriaId, nome, ignorarId);
        }
    }
}
