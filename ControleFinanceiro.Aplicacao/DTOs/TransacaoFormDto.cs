

using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Aplicacao.DTOs
{
    public class TransacaoFormDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public int CategoriaId { get; set; }

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres.")]
        public string? Observacoes { get; set; }

        // Para dropdown
        public IEnumerable<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();
    }
}
