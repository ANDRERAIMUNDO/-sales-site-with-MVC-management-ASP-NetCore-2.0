using LojaVirtual.Data;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using LojaVirtual.Repositories.Contracts;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Repositories.Contracts
{
    public class CategoriaRepository : ICategoriaRepository
    {
        //const int _registroPorPagina = 20;
        IConfiguration _configuration;
        LojaVirtualContext _banco;
        public CategoriaRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }
        public void Atualizar(Categoria categoria)
        {
            _banco.Update(categoria);
            _banco.SaveChanges();
        }

        public void Cadastrar(Categoria categoria)
        {
            _banco.Add(categoria);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
           Categoria categoria = obterCategoria(Id);
            _banco.Remove(categoria);
            _banco.SaveChanges();
        }

        public Categoria obterCategoria(int id)
        {
            return _banco.Categorias.Find(id);
        }

        public IPagedList<Categoria> ObterTodasCategorias(int? pagina)
        {
            int RegistroPorPagina = _configuration.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina?? 1;
            //  return _banco.Categorias.Include(a=>a.CategoriaPai).ToPagedList<Categoria>(NumeroPagina, _registroPorPagina);
            return _banco.Categorias.Include(a => a.CategoriaPai).ToPagedList<Categoria>(NumeroPagina, RegistroPorPagina);
        }
        public IEnumerable<Categoria> ObterTodasCategorias()
        {
            return _banco.Categorias;   
        }
    }
}
