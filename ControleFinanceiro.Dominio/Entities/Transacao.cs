using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
    public  class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        public string? Observacoes { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Navegação
        public Categoria Categoria { get; set; } = null!;
    }
}
