using ControleFinanceiro.Aplicacao.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DashboardService _dashboardService;

        public HomeController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _dashboardService.ObterDashboardAsync();
            return View(dashboard);
        }
    }
}