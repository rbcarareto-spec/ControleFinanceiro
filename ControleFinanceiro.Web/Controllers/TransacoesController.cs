using ControleFinanceiro.Aplicacao.DTOs;
using ControleFinanceiro.Aplicacao.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class TransacoesController : Controller
    {
        private readonly TransacaoService _transacaoService;
        private readonly CategoriaService _categoriaService;

        public TransacoesController(TransacaoService transacaoService, CategoriaService categoriaService)
        {
            _transacaoService = transacaoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var transacoes = await _transacaoService.ObterTodasAsync();
            return View(transacoes);
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _transacaoService.ObterCategoriasAtivasAsync();
            var model = new TransacaoFormDto
            {
                Data = DateTime.Today,
                Categorias = categorias.Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Tipo,
                    Ativo = c.Ativo
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransacaoFormDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new TransacaoDto
                    {
                        Descricao = model.Descricao,
                        Valor = model.Valor,
                        Data = model.Data,
                        CategoriaId = model.CategoriaId,
                        Observacoes = model.Observacoes
                    };

                    await _transacaoService.AdicionarAsync(dto);
                    TempData["MensagemSucesso"] = "Transação cadastrada com sucesso!";
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

            // Recarregar categorias em caso de erro
            model.Categorias = (await _categoriaService.ObterTodasAsync())
                .Where(c => c.Ativo)
                .Select(c => new CategoriaDto { Id = c.Id, Nome = c.Nome });

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transacao = await _transacaoService.ObterPorIdAsync(id);
            if (transacao == null) return NotFound();

            var categorias = await _transacaoService.ObterCategoriasAtivasAsync();
            var model = new TransacaoFormDto
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Data = transacao.Data,
                CategoriaId = transacao.CategoriaId,
                Observacoes = transacao.Observacoes,
                Categorias = categorias.Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Tipo
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransacaoFormDto model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var transacaoDto = new TransacaoDto
                    {
                        Id = model.Id,
                        Descricao = model.Descricao,
                        Valor = model.Valor,
                        Data = model.Data,
                        CategoriaId = model.CategoriaId,
                        Observacoes = model.Observacoes
                    };

                    await _transacaoService.AtualizarAsync(transacaoDto);

                    TempData["MensagemSucesso"] = "Transação atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex) 
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao salvar. Tente novamente.");
                }
            }

            // Recarregar categorias mesmo em caso de erro (para manter o dropdown)
            var categorias = await _categoriaService.ObterTodasAsync();
            model.Categorias = categorias
                .Where(c => c.Ativo)
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nome = c.Nome
                });

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transacao = await _transacaoService.ObterPorIdAsync(id);
            if (transacao == null) return NotFound();
            return View(transacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transacaoService.ExcluirAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}