using DEVLOOM_CATALOG.Domain;

namespace DEVLOOM_CATALOG.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> AdicionaAsync(Categoria categoria);
        Task<bool> AtualizarAsync(Categoria categoria);
        Task<Categoria?> BuscaPorIdAsync(Guid id);
        Task<Categoria?> BuscaPorNomeAsync(string nome);
        Task<IEnumerable<Categoria>> RecuperaTodasAsync();
        Task<bool> DeletarAsync(Guid id);
        Task<bool> ExisteNomeAsync(string nome, Guid? ignorarId = null);
    }
}
