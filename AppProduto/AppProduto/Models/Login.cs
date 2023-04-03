using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProduto.Models
{
    internal class Login
    {

        public string Nome { get; set; }
        public string Senha { get; set; }

        public Login(){}

        public Login(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }
    }
}
