using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Aplicacao.Services
{
    public class DashboardService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public DashboardService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<DashboardDto> ObterDashboardAsync()
        {
            // Opção 1: Usando métodos específicos (recomendado para performance)
            var totalReceitas = await _transacaoRepository.ObterSomaPorTipoAsync(TipoTransacao.Receita);
            var totalDespesas = await _transacaoRepository.ObterSomaPorTipoAsync(TipoTransacao.Despesa);


            return new DashboardDto
            {
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                Saldo = totalReceitas - totalDespesas
            };
        }

    }
}