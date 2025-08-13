using System.Data;
using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Infra.Interfaces;
using Dapper;

namespace DEVLOOM_CATALOG.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IDbConnection _connection;

        public CategoriaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AdicionaAsync(Categoria categoria)
        {
            string sql = "INSERT INTO Categorias (Nome, DataCriacao, Ativo) VALUES (@Nome, SYSUTCDATETIME(), @Ativo)";

            var parametros = new
            {
                categoria.Nome,
                categoria.Ativo
            };

            var resultado = await _connection.ExecuteAsync(sql, parametros);
            return resultado > 0;
        }

        public async Task<bool> AtualizarAsync(Categoria categoria)
        {
            string sql = @"
                UPDATE Categorias 
                SET Nome = @Nome, 
                    DataAtualizacao = SYSUTCDATETIME(),
                    Ativo = @Ativo
                WHERE Id = @Id";

            var parametros = new
            {
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                Id = categoria.Id
            };

            var resultado = await _connection.ExecuteAsync(sql, parametros);
            return resultado > 0;
        }

        public async Task<Categoria?> BuscaPorIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Categorias WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { Id = id });
        }

        public async Task<Categoria?> BuscaPorNomeAsync(string nome)
        {
            string sql = "SELECT * FROM Categorias WHERE Nome = @Nome";
            return await _connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { Nome = nome });
        }

        public async Task<IEnumerable<Categoria>> RecuperaTodasAsync()
        {
            string sql = "SELECT * FROM Categorias";
            return await _connection.QueryAsync<Categoria>(sql);
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            string sql = "DELETE FROM Categorias WHERE Id = @Id";
            var resultado = await _connection.ExecuteAsync(sql, new { Id = id });
            return resultado > 0;
        }

        public async Task<bool> ExisteNomeAsync(string nome, Guid? ignorarId = null)
        {
            string sql = "SELECT COUNT(1) FROM Categorias WHERE Nome = @Nome";
            if (ignorarId.HasValue)
                sql += " AND Id != @Id";

            var parametros = new
            {
                Nome = nome,
                Id = ignorarId
            };

            var count = await _connection.ExecuteScalarAsync<int>(sql, parametros);
            return count > 0;
        }
    }
}
