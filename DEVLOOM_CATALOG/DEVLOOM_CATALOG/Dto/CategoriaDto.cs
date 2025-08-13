namespace DEVLOOM_CATALOG.Application.DTOs
{
    // DTO para criar ou atualizar categoria
    public class CategoriaRequestDto
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }

    // DTO para retornar categoria ao frontend
    public class CategoriaResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }

        public List<ProdutoResponseDto>? Produtos { get; set; }
    }


}
