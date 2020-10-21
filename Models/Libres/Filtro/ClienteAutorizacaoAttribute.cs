using LojaVirtual.Models.Libres.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Filtro
{
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();
            if(cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso negaddo" };
            }
        }
    }
}