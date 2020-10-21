using LojaVirtual.Models.Libres.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Display(Name ="Nome")]
        [Required(ErrorMessageResourceType = typeof (Mensagem), ErrorMessageResourceName = "ERRO")]
        public string Nome { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "ERRO")]
        public string Slug { get; set; }

        [Display(Name ="Categoria Pai")]
        public int? CategoriaPaID { get; set; }


        [ForeignKey("CategoriaPaID")]
        public virtual Categoria CategoriaPai {get;set; }
    }
}
//relacionamento de categorias entre produtos o tipos de relacionamento