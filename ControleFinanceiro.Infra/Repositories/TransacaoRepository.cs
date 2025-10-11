using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Enums;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Transacao transacao)
        {
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao != null)
            {
                _context.Transacoes.Remove(transacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
            => await _context.Transacoes.AnyAsync(t => t.Id == id);

        public async Task<Transacao?> ObterPorIdAsync(int id)
            => await _context.Transacoes.Include(t => t.Categoria).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<Transacao>> ObterTodasAsync()
            => await _context.Transacoes.Include(t => t.Categoria).ToListAsync();

        public async Task<decimal> ObterSomaPorTipoAsync(TipoTransacao tipo)
        {
            return await _context.Transacoes
                .Include(t => t.Categoria)
                .Where(t => t.Categoria.Tipo == tipo)
                .SumAsync(t => t.Valor);
        }

        public async Task<IEnumerable<Transacao>> ObterTodasComCategoriaAsync()
        {
            return await _context.Transacoes
                .Include(t => t.Categoria)
                .ToListAsync();
        }
    }
}
