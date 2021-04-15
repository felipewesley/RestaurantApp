using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Repositorio.Services.Cozinha.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Senha { get; set; }

        public void Validar()
        {
            if (Username.Length == 0 || Senha.Length == 0)
                throw new Exception("Nome de usuario e senha sao obrigatorios para login");
        }
    }
}
