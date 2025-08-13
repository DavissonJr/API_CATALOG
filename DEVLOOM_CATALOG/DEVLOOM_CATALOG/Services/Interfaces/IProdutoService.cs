using DEVLOOM_CATALOG.Domain;

namespace DEVLOOM_CATALOG.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<bool> AdicionaAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        Task<Produto?> BuscaPorIdAsync(Guid id);
        Task<RetornoPaginado<Produto>> BuscarPorCategoriaPaginadoAsync(Guid categoriaId, int pagina, int quantidade);
        Task<bool> DeletarAsync(Guid id);
        Task<bool> ExisteNomeNaCategoriaAsync(Guid categoriaId, string nome, Guid? ignorarId = null);
    }
}
