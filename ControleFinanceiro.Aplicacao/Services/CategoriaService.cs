using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Aplicacao.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaDto>> ObterTodasAsync() => await _categoriaRepository.ObterTodasAsync();
        public async Task<CategoriaDto?> ObterPorIdAsync(int id) => await _categoriaRepository.ObterPorIdAsync(id);
        public async Task AdicionarAsync(CategoriaDto categoria)
        {
            var existente = await _categoriaRepository.ObterTodasAsync();
            if (existente.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Já existe uma categoria com esse nome.");

            await _categoriaRepository.AdicionarAsync(categoria);
        }
        public async Task AtualizarAsync(CategoriaDto categoria) => await _categoriaRepository.AtualizarAsync(categoria);
        public async Task<bool> ExisteAsync(int id) => await _categoriaRepository.ExisteAsync(id);
    }
}
