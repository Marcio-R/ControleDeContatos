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
            await _repositorio.Adicionar(contato);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult CancelaExclusao()
        {
            return View();
        }
    }
}
