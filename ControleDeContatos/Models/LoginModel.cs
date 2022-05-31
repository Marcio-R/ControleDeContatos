using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
    }
}
