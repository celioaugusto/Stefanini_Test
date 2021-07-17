using System;
using System.Collections.Generic;
using System.Text;

namespace Stefanini_Test.PageObjects
{
    public class CadastroUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public CadastroUsuario() { }

        public CadastroUsuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

    }
}
