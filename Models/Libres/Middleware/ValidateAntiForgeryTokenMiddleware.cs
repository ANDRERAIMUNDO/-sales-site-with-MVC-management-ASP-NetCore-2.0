﻿using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

       public ValidateAntiForgeryTokenMiddleware (RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }
        public async Task Invoke (HttpContext context)
        {
              if (HttpMethods.IsPost(context.Request.Method))
            {
              await _antiforgery.ValidateRequestAsync(context);
            }
            await _next(context);
        }
    }
}
