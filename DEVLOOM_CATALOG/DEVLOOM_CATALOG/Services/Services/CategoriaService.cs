using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Infra.Interfaces;
using DEVLOOM_CATALOG.Services.Interfaces;
using AutoMapper;

namespace DEVLOOM_CATALOG.Services.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<bool> AdicionaAsync(Categoria categoria)
        {
            return await _categoriaRepository.AdicionaAsync(categoria);
        }

        public async Task<bool> AtualizarAsync(Categoria categoria)
        {
            return await _categoriaRepository.AtualizarAsync(categoria);
        }

        public async Task<Categoria?> BuscaPorIdAsync(Guid id)
        {
            return await _categoriaRepository.BuscaPorIdAsync(id);
        }

        public async Task<Categoria?> BuscaPorNomeAsync(string nome)
        {
            return await _categoriaRepository.BuscaPorNomeAsync(nome);
        }

        public async Task<IEnumerable<Categoria>> RecuperaTodasAsync()
        {
            return await _categoriaRepository.RecuperaTodasAsync();
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            return await _categoriaRepository.DeletarAsync(id);
        }

        public async Task<bool> ExisteNomeAsync(string nome, Guid? ignorarId = null)
        {
            return await _categoriaRepository.ExisteNomeAsync(nome, ignorarId);
        }
    }
}
