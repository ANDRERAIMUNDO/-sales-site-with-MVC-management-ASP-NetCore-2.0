using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Libres.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public GerenciarEmail (SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {
            string corpoMsg = string.Format(
                "<h4> Contato - Loja Virtual</12>" +
                "<b>Nome: </b>{0} <br/>"+
                "<b>Email: </b>{1} <br/>" +
                "<b>Texto: </b>{2} <br/>" +
                "<b>Email Enviado automaticamente - não responda </b>",
                contato.Nome,
                contato.Email,
                contato.Texto
                );
       
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username")); // enviado por 9000andre@gmail.com
            mensagem.To.Add("9000andre@gmail.com");//envia para andreraimundoo@hotmail.com
            mensagem.Subject = "Contato LojaVirtual - Email " + contato.Email; //assunto do email
            mensagem.Body = corpoMsg;//corpo da menssagem o conteudo da mensagem
            mensagem.IsBodyHtml = true; //validar uso de html contigo na variavel corpoMsg

            _smtp.Send(mensagem);
        }
        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string Msg = string.Format(
                colaborador.Senha
                );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:UserName")); 
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador LojaVirtual - Email " + colaborador.Email; 
            mensagem.Body = Msg;
            mensagem.IsBodyHtml = true; 

            _smtp.Send(mensagem);
        }
    }
}
