using System;

namespace Restaurante.Repositorio.Services.Cozinha.Models
{
    public class RegistrarModel
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string PerguntaSeguranca { get; set; }
    }
}
