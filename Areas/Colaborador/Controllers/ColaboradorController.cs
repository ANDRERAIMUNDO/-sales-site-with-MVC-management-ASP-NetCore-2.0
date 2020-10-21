using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList;
using LojaVirtual.Models.Libres.Texto;
using LojaVirtual.Models.Libres.Email;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using LojaVirtual.Models.Libres.Filtro;
using LojaVirtual.Models.Constants;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacaoAtribute(ColaboradorTipoConstants.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;
        public ColaboradorController(IColaboradorRepository colaboradorRepository, GerenciarEmail gerenciarEmail)
        {
            _colaboradorRepository = colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Colaborador> colaboradores = _colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");//ignora senha
            ModelState.Remove ("ConfirmacaoSenha");//ignora confirma senha
            if (ModelState.IsValid)
            {
                colaborador.Tipo = ColaboradorTipoConstants.Comum;
                colaborador.Senha = KeyGenerator.GetRandomNumber(5);
                _colaboradorRepository.Cadastrar(colaborador);
                _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

                TempData["MSG"] = "Registro realizado com sucesso";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GerarSenha(int id)
        {
            Models.Colaborador colaborador =  _colaboradorRepository.ObterColaborador(id); //buscar no banco de dados
            colaborador.Senha = KeyGenerator.GetRandomNumber(5); //gera senha aleatoria de 8 bits
            _colaboradorRepository.AtualizarSenha(colaborador);//salva senha aleatoria no banco de dados

            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);//envia senha aleatoria para o email

            TempData["MSG"] = "Verifique seu email, enviamos uma nova senha";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int id)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");

            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG"] = "Registro alterado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            TempData["MSG"] = "Registro excluido com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}

