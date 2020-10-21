using LojaVirtual.Models.Libres.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Areas.Colaborador;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using LojaVirtual.Models.Constants;

namespace LojaVirtual.Models.Libres.Filtro
{
    public class ColaboradorAutorizacaoAtribute : Attribute, IAuthorizationFilter
    {
        private string _TipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAtribute( string TipoColaboradorAutorizado = ColaboradorTipoConstants.Comum)
        {
            _TipoColaboradorAutorizado = TipoColaboradorAutorizado;
        }
        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(
                typeof(LoginColaborador));
            Colaborador colaborador = _loginColaborador.GetColaborador();
            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);  
            }
            else
            {
                if (colaborador.Tipo == ColaboradorTipoConstants.Comum && _TipoColaboradorAutorizado == ColaboradorTipoConstants.Gerente)
                {
                    context.Result = new ForbidResult();//retorna ao estado negado
                }
            }
        }
    }
}
