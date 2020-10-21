using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Visualizar()
        {
            Produto Produto = GetProduto();
            return View(Produto);
        }
        private Produto GetProduto()
        {
            return new Produto
                {
                Id = 1,
                Nome = "Xbox One",
                Descricao = "jogue online",
                Valor = 200.00M
            };   
        }

    }
}
