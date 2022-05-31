using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioRepositorio _repositorio;

        public UsuariosController(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            var obj = await _repositorio.TrazerTodos();
            return View(obj);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario Cadastrado";
                    return RedirectToAction(nameof(Index));
                }
                return View(usuario);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuario, tente novamente:{error.Message} ";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var obj = await _repositorio.BuscarPorId(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario Atualizado";
                    return RedirectToAction(nameof(Index));
                }
                return View("Editar", usuario);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar usúario {error.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> TelaExclusao(int id)
        {
            var obj = await _repositorio.BuscarPorId(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                await _repositorio.Excluir(id);
                TempData["MensagemSucesso"] = $"Usuario apagado";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao excluir usúario {error.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
