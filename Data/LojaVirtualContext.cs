using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;
namespace LojaVirtual.Data
{
    public class LojaVirtualContext: DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext>options)
            : base(options)
        {

        }
        public DbSet<LojaVirtual.Models.NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<LojaVirtual.Models.Produto> Produtos { get; set; }
        public DbSet<LojaVirtual.Models.Cliente> Clientes { get; set; }
        public DbSet<LojaVirtual.Models.Contato> Contatos { get; set; }
        public DbSet<LojaVirtual.Models.Colaborador> Colaboradores { get; set; }
        public DbSet<LojaVirtual.Models.Categoria> Categorias { get; set; }
    }
}
