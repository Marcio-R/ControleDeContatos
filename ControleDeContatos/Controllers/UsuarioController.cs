using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _repositorio;

        public UsuarioController(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task <IActionResult> Index()
        {
            var obj = await _repositorio.TrazerTodos();
            return View(obj);
        }

        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CriarUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
                return View (usuario);
            }
            catch ( Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente:{erro.Message} ";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
