
namespace ControleFinanceiro.Aplicacao.DTOs
{
    public class DashboardDto
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
        public List<TransacaoDto> UltimasTransacoes { get; set; } = new();
    }
}
