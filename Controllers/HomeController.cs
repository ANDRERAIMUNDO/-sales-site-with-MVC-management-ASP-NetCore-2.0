using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Models.Libres.Email;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.Data;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Models.Libres.Login;
using LojaVirtual.Models.Libres.Filtro;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsLetterRepository _repositoryNewsLetter;
        private LoginCliente _logincliente;
        private GerenciarEmail _gerenciarEmail;
        public HomeController(IClienteRepository repositoryCliente, INewsLetterRepository repositoryNewsLetter,
            LoginCliente loginCliente, GerenciarEmail gerenciarEmail)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLetter = repositoryNewsLetter;
            _logincliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletter) //sobrecaraga do metodo ContatoAcao
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsLetter.Cadastrar(newsletter);
                TempData["MSG_S"] = "Parabens ! Fique antento nas nossas ofertas!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato
                {
                    Nome = HttpContext.Request.Form["Nome"],
                    Email = HttpContext.Request.Form["Email"],
                    Texto = HttpContext.Request.Form["Texto"]
                };
                var listaMensagem = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagem, true);

                if (isValid)
                {
                    _gerenciarEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagem)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Algo de errado aconteceu de novo";
            }
            return View("Contato");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);

                if (clienteDB != null)
                {
                    _logincliente.Login(clienteDB);
                    return new RedirectResult(Url.Action(nameof(Painel)));
                }
            else
                {
                ViewData["MSG_S"] = "usuario ou senha incorretos";
                return View(); 
                }
            }
        [HttpGet]
        [ClienteAutorizacao]
        public IActionResult Painel()
        {
            return new ContentResult() { Content = "Painel do cliente" };
        }
        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm] Cliente cliente)
        {
            if (ModelState.IsValid)//validado de cadastro no banco de dados
            {
                _repositoryCliente.Cadastrar(cliente);
                TempData["MSG_S"] = "Cadastro realizado";
                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }
        public IActionResult CarrinhoCompra()
        {
            return View();
        }

    }
}
