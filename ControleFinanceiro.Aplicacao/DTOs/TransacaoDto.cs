using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Aplicacao.DTOs
{
    public class TransacaoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        public string? Observacoes { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
        public TipoTransacao TipoCategoria { get; set; }
    }
}
