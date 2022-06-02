using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ControleDeContatos.Helper
{
    public class RepositorioSessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public RepositorioSessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task CriarSessao(Usuario usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public async Task<Usuario> BuscarSessao()
        {
            string sessaUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaUsuario))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Usuario>(sessaUsuario);

        }

        public async Task RemoverSessao()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }

    }
}
