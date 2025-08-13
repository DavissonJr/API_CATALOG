using DEVLOOM_CATALOG.Domain;

namespace DEVLOOM_CATALOG.Infra.Interfaces
{
    public interface IProdutoRepository
    {
        Task<RetornoPaginado<Produto>> BuscarPorCategoriaPaginadoAsync(Guid categoriaId, int pagina, int quantidade);
        Task<Produto?> BuscaPorIdAsync(Guid id);
        Task<bool> AdicionaAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        Task<bool> DeletarAsync(Guid id);
        Task<bool> ExisteNomeNaCategoriaAsync(Guid categoriaId, string nome, Guid? ignorarId = null);
    }
}
