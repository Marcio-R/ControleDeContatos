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

        public async Task<IActionResult> Editar(int id)
        {
            var obj = await _repositorio.ListaPorId(id);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(int id,Contato contato)
        {
            if(id != contato.Id)
            {
                return BadRequest();
            }
            await _repositorio.Altualizar(contato);
            return RedirectToAction(nameof(Index)); 
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
