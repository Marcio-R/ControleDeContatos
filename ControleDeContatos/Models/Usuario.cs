using ControleDeContatos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do contato")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfirl do contato")]
        public PerfilEnum? Perfil { get; set; }

        [Required(ErrorMessage = "Digite o senha do contato")]
        public string? Senha { get; set; }

    }
}

