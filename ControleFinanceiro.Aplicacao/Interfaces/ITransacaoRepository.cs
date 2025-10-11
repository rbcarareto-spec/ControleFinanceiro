using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ObterTodasAsync();
        Task<Transacao?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Transacao transacao);
        Task AtualizarAsync(Transacao transacao);
        Task ExcluirAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<decimal> ObterSomaPorTipoAsync(TipoTransacao tipo);
        Task<IEnumerable<Transacao>> ObterTodasComCategoriaAsync();
    }
}
