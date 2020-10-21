using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Filtro
{
    public class ValidateHttpRefererAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //execultado antes de passar pelo controlador
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult() { Content = "Acesso negado" };
            }
            else
            {
                Uri uri = new Uri(referer);

                string hostReferer = uri.Host;
                string hostServidor = context.HttpContext.Request.Host.Host;
                
                if (hostReferer != hostServidor)
                {
                    context.Result = new ContentResult() { Content = "Acesso negado!" };
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //execultado apos de passa pelo controlador
            throw new NotImplementedException();
        }
    }
}
