using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public LoginController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = await _usuarioRepositorio.BuscarLogin(loginModel.Login);

                    if (usuario.Login == loginModel.Login && usuario.Senha == loginModel.Senha)
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    TempData["MensagemError"] = $"Login ou senha invalido";
                }
                return View("Index");
            }
            catch (Exception error)
            {
                TempData["MensagemError"] = $"Ops Erro de logon {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
