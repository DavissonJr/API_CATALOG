using DEVLOOM_CATALOG.Domain;

namespace DEVLOOM_CATALOG.Infra.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> RecuperaTodasAsync();
        Task<Categoria?> BuscaPorIdAsync(Guid id);
        Task<Categoria?> BuscaPorNomeAsync(string nome);
        Task<bool> AdicionaAsync(Categoria categoria);
        Task<bool> AtualizarAsync(Categoria categoria);
        Task<bool> DeletarAsync(Guid id);
        Task<bool> ExisteNomeAsync(string nome, Guid? ignorarId = null);
    }
}
