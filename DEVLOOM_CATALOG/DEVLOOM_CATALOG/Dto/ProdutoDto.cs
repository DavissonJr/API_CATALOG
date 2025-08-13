namespace DEVLOOM_CATALOG.Application.DTOs
{
    // DTO para criar ou atualizar produto
    public class ProdutoRequestDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public Guid CategoriaId { get; set; }
        public bool Ativo { get; set; } = true;
    }

    // DTO para retornar produto ao frontend
    public class ProdutoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public Guid CategoriaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }

        // Se quiser incluir informações da categoria
        public CategoriaResponseDto? Categoria { get; set; }
    }
}
