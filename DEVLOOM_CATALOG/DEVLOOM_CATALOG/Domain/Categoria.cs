namespace DEVLOOM_CATALOG.Domain
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }

        // Relacionamento 1:N
        public List<Produto> Produtos { get; set; } = new();
    }
}

