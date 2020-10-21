using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int Id);
        Categoria obterCategoria(int id);
        IEnumerable<Categoria> ObterTodasCategorias();
        IPagedList<Categoria> ObterTodasCategorias(int? pagina);
    }
}
