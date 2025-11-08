using ControleFinanceiro.Aplicacao.Services;
using ControleFinanceiro.Aplicacao.DTOs;
using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObterTodasAsync();
            return View(categorias);
        }

        public IActionResult Create()
        {
            var model = new CategoriaDto
            {
                Ativo = true //Define como "checado" por padrão
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaDto dto)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    await _categoriaService.AdicionarAsync(dto);

                    TempData["MensagemSucesso"] = "Categoria cadastrada com sucesso Parabens!";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado. Tente novamente.");
                }
            }

            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaDto categoria)
        {
            if (id != categoria.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _categoriaService.AtualizarAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
    }
}