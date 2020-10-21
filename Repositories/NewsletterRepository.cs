using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using LojaVirtual.Data;

namespace LojaVirtual.Repositories
{
    public class NewsletterRepository : INewsLetterRepository
    {
        private LojaVirtualContext _banco;
        public NewsletterRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }
        public void Cadastrar(NewsletterEmail newsletter)
        {
            _banco.NewsletterEmails.Add(newsletter);
            _banco.SaveChanges();
          //  throw new NotImplementedException();
        }

        public List<NewsletterEmail> ObterTodasnewsletter()
        {
           return _banco.NewsletterEmails.ToList();
            throw new NotImplementedException();
        }
    }
}
