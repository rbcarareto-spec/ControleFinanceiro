using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Enums;


namespace ControleFinanceiro.Aplicacao.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<TransacaoDto>> ObterTodasAsync()
        {
            var transacoes = await _transacaoRepository.ObterTodasAsync();

            return transacoes.Select(t => new TransacaoDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Data = t.Data,
                CategoriaId = t.CategoriaId,
                Observacoes = t.Observacoes,
                NomeCategoria = t.Categoria?.Nome ?? "Sem categoria",
                TipoCategoria = t.Categoria?.Tipo ?? TipoTransacao.Despesa
            });
        }
        public async Task<TransacaoDto?> ObterPorIdAsync(int id)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(id);

            if (transacao == null)
                return null;

            return new TransacaoDto
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Data = transacao.Data,
                CategoriaId = transacao.CategoriaId,
                Observacoes = transacao.Observacoes,
                NomeCategoria = transacao.Categoria?.Nome ?? string.Empty,
                TipoCategoria = transacao.Categoria?.Tipo ?? TipoTransacao.Despesa
            };
        }
        public async Task AdicionarAsync(TransacaoDto dto)
        {
            // Validação: Data não pode ser futura
            if (dto.Data.Date > DateTime.Today)
                throw new ArgumentException("A data da transação não pode ser futura.");

            var transacao = new Transacao
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Data = dto.Data,
                CategoriaId = dto.CategoriaId,
                Observacoes = dto.Observacoes,
                DataCriacao = DateTime.UtcNow // ou DateTime.Now, conforme sua regra
            };

            await _transacaoRepository.AdicionarAsync(transacao);
        }
        public async Task AtualizarAsync(TransacaoDto dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("ID da transação inválido.");

            // Validação: Data não pode ser futura
            if (dto.Data.Date > DateTime.Today)
                throw new ArgumentException("A data da transação não pode ser futura.");


            var transacao = new Transacao
            {
                Id = dto.Id,
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Data = dto.Data,
                CategoriaId = dto.CategoriaId,
                Observacoes = dto.Observacoes,

            };

            await _transacaoRepository.AtualizarAsync(transacao);
        }
        public async Task ExcluirAsync(int id) => await _transacaoRepository.ExcluirAsync(id);
        public async Task<bool> ExisteAsync(int id) => await _transacaoRepository.ExisteAsync(id);
        public async Task<IEnumerable<CategoriaDto>> ObterCategoriasAtivasAsync() =>
            (await _categoriaRepository.ObterTodasAsync()).Where(c => c.Ativo).ToList();

    }
}
