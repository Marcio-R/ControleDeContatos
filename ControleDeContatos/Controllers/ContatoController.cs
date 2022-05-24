using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{

    public class ContatoController : Controller
    {
        private readonly ContatoRepositorio _repositorio;

        public ContatoController(ContatoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            var obj = await _repositorio.BuscarTodos();
            return View(obj);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction(nameof(Index));

                }

                return View(contato);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente:{error.Message} ";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var obj = await _repositorio.ListaPorId(id);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(int id, Contato contato)
        {
            if (id != contato.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await _repositorio.Altualizar(contato);
                return RedirectToAction(nameof(Index));

            }
            //Forçando o return a cair na View Editar e mais o obj contato.
            return View("Editar", contato);
        }

        public async Task<IActionResult> CancelaExclusao(int id)
        {
            var obj = await _repositorio.ListaPorId(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apagar(int id)
        {
            await _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
