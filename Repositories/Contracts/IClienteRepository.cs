using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Contracts
{
   public interface IClienteRepository
    {
        Cliente Login(string Email, string Senha);

        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int Id);
        Cliente ObterCliente(int id);
        IEnumerable<Cliente> ObterTodosClientes();
    }
}
