using ControleFinanceiro.Aplicacao.DTOs;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriaDto>> ObterTodasAsync();
        Task<CategoriaDto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(CategoriaDto categoria);
        Task AtualizarAsync(CategoriaDto categoria);
        Task<bool> ExisteAsync(int id);
    }
}