using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoTransacao Tipo { get; set; }
        public bool Ativo { get; set; } = true;

        // Navegação
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
