using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Sessao
{
    public class Sessao
    {
       private IHttpContextAccessor _context;
        public Sessao(IHttpContextAccessor context) //injeção de dependencia para IHttpcontext
        {
            _context = context;
        }
        public void Cadastrar( string key, string valor)
        {
            _context.HttpContext.Session.SetString(key, valor);
        }
        public void Atualizar (string key, string valor)
        {
            if (Existe(key))
            {
                _context.HttpContext.Session.Remove(key);
            }
            _context.HttpContext.Session.GetString(key);
        }
        public void Remover (string key)
        {
            _context.HttpContext.Session.Remove(key);
        }
        public string Consulta(string key)
        {
            return _context.HttpContext.Session.GetString(key);
        }
        public bool Existe (string key)
        {
           if ( _context.HttpContext.Session.GetString(key) == null)
            {
                return false;
            }
           else
            {
                return true;
            }
        }
        public void RemoverTodos()
        {
            _context.HttpContext.Session.Clear();
        }
    }
}
