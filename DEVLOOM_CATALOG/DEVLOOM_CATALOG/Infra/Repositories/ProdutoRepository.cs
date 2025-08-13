using System.Data;
using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Infra.Interfaces;
using Dapper;

namespace DEVLOOM_CATALOG.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _connection;

        public ProdutoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AdicionaAsync(Produto produto)
        {
            string sql = @"
                INSERT INTO Produtos (Nome, PrecoUnitario, CategoriaId, DataCriacao, Ativo)
                VALUES (@Nome, @PrecoUnitario, @CategoriaId, SYSUTCDATETIME(), @Ativo)";

            var parametros = new
            {
                produto.Nome,
                produto.PrecoUnitario,
                produto.CategoriaId,
                produto.Ativo
            };

            var resultado = await _connection.ExecuteAsync(sql, parametros);
            return resultado > 0;
        }

        public async Task<bool> AtualizarAsync(Produto produto)
        {
            string sql = @"
                UPDATE Produtos
                SET Nome = @Nome,
                    PrecoUnitario = @PrecoUnitario,
                    CategoriaId = @CategoriaId,
                    DataAtualizacao = SYSUTCDATETIME(),
                    Ativo = @Ativo
                WHERE Id = @Id";

            var parametros = new
            {
                produto.Nome,
                produto.PrecoUnitario,
                produto.CategoriaId,
                produto.Ativo,
                produto.Id
            };

            var resultado = await _connection.ExecuteAsync(sql, parametros);
            return resultado > 0;
        }

        public async Task<Produto?> BuscaPorIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Produtos WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
        }

        public async Task<RetornoPaginado<Produto>> BuscarPorCategoriaPaginadoAsync(Guid categoriaId, int pagina, int quantidade)
        {
            string sql = @"
                SELECT * FROM Produtos
                WHERE CategoriaId = @CategoriaId
                ORDER BY Nome
                OFFSET @Offset ROWS FETCH NEXT @Quantidade ROWS ONLY";

            var parametros = new
            {
                CategoriaId = categoriaId,
                Offset = (pagina - 1) * quantidade,
                Quantidade = quantidade
            };

            var produtos = await _connection.QueryAsync<Produto>(sql, parametros);

            string sqlCount = "SELECT COUNT(*) FROM Produtos WHERE CategoriaId = @CategoriaId";
            var totalRegistros = await _connection.ExecuteScalarAsync<int>(sqlCount, new { CategoriaId = categoriaId });

            return new RetornoPaginado<Produto>
            {
                TotalRegistros = totalRegistros,
                Pagina = pagina,
                QtdPagina = quantidade,
                Retorno = produtos.ToList()
            };
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            string sql = "DELETE FROM Produtos WHERE Id = @Id";
            var resultado = await _connection.ExecuteAsync(sql, new { Id = id });
            return resultado > 0;
        }

        public async Task<bool> ExisteNomeNaCategoriaAsync(Guid categoriaId, string nome, Guid? ignorarId = null)
        {
            string sql = "SELECT COUNT(1) FROM Produtos WHERE CategoriaId = @CategoriaId AND Nome = @Nome";
            if (ignorarId.HasValue)
                sql += " AND Id != @Id";

            var parametros = new
            {
                CategoriaId = categoriaId,
                Nome = nome,
                Id = ignorarId
            };

            var count = await _connection.ExecuteScalarAsync<int>(sql, parametros);
            return count > 0;
        }
    }
}
