using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Models.Libres.Filtro;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
   [ColaboradorAutorizacaoAtribute]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;
        public CategoriaController (ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index(int? pagina)
        {
            var categoria = _categoriaRepository.ObterTodasCategorias(pagina);     
            return View(categoria);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a =>
            new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Categoria categoria)//FromForm ? receber dados do formulario
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Cadastrar(categoria);
                TempData["MSG"] = "registro salvo com sucesso";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a =>
                new SelectListItem(a.Nome, a.Id.ToString()));
                return View();
        }   
        [HttpGet]
        public IActionResult Atualizar(int id) 
        {
           var categoria = _categoriaRepository.obterCategoria(id);
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Where(a =>
            a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Categoria categoria, int id)
        {
            if (ModelState.IsValid) 
            { 
                _categoriaRepository.Atualizar(categoria);
                TempData["MSG"] = "registro alterado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Where(a =>
            a.Id != id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _categoriaRepository.Excluir(id);
            TempData["MSG"] = "registro excluido com sucesso";
            return RedirectToAction(nameof(Index));
        }

       
    }
}
