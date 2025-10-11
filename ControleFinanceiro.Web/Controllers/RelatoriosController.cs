using ControleFinanceiro.Aplicacao.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class RelatoriosController : Controller
    {
        public readonly RelatorioService _relatorioService;
        public RelatoriosController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }
        public IActionResult SaldoPorPeriodo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaldoPorPeriodo(DateTime? dataInicio, DateTime? dataFim, string? action)
        {
            if (dataInicio.HasValue && dataFim.HasValue && dataInicio > dataFim)
            {
                ModelState.AddModelError("", "A data inicial não pode ser maior que a data final.");
                return View();
            }

            var relatorio = await _relatorioService.GerarRelatorioAsync(dataInicio, dataFim);

            // Se o usuário clicou em "Exportar Excel"
            if (action == "exportar")
            {
                var arquivoExcel = _relatorioService.GerarExcel(relatorio);
                var nomeArquivo = $"RelatorioSaldo_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }

            return View(relatorio);
        }
    }
}
