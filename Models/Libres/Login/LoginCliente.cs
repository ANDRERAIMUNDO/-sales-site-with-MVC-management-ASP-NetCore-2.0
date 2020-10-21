using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaVirtual.Models.Libres.Login
{
    public class LoginCliente
    {
        private string Key = "LoginCliente";
        private Sessao.Sessao _sessao;
        public LoginCliente (Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }
        public void Login (Cliente cliente)
        {
            //Serializar
            string clienteJSonString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(Key, clienteJSonString);
        }
        public Cliente GetCliente()
        {
            //Deserializar
            if (_sessao.Existe(Key))//sessao existe ?
            {
                string clienteJSonString = _sessao.Consulta(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJSonString);
            }
            else
            {
                return null;
            }
        }
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
