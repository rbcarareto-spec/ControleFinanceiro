using ControleFinanceiro.Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Aplicacao.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        public TipoTransacao Tipo { get; set; }
        public bool Ativo { get; set; }
        public string NomeExibicao => $"{Nome} ({(Tipo == TipoTransacao.Receita ? "Receita" : "Despesa")})";
    }
}
