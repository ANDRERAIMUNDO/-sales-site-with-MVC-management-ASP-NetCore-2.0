using LojaVirtual.Models.Libres.Lang;
using LojaVirtual.Models.Libres.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        [EmailUnicoColaborador]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        public string Senha { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        [NotMapped]//não vai para o banco de dados
        [Compare("Senha", ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERROSENHA")]
        public string ConfirmacaoSenha { get; set; }
        public string Tipo { get; set; }
    }
}
//Colaborador  = ColaboradorTipoConstant