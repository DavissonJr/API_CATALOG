namespace DEVLOOM_CATALOG.Domain
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public Guid CategoriaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }

        // Relacionamento N:1
        public Categoria? Categoria { get; set; }
    }
}
