using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        private readonly RepositorioSessao _repositorioSessao;

        public LoginController(UsuarioRepositorio usuarioRepositorio, RepositorioSessao repositorioSessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _repositorioSessao = repositorioSessao;
        }

        public IActionResult Index()
        {
            // Se o usuario estiver logado, redirecionar para a home
            if (_repositorioSessao.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Sair()
        {
            _repositorioSessao.RemoverSessao();
            return RedirectToAction("Index", "Login");
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
                        _repositorioSessao.CriarSessao(usuario);
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
