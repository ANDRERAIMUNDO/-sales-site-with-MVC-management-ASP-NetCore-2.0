using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Data;
using LojaVirtual.Repositories.Contracts;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext _banco;//aplicando injeção de dependencia
        public ClienteRepository(LojaVirtualContext banco)//aplicando injeção de dependencia
        {
            _banco = banco;//aplicando injeção de dependencia
        }
        public void Atualizar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
            throw new NotImplementedException();
        }

        public void Cadastrar(Cliente cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Cliente cliente = ObterCliente(Id);
            _banco.Remove(cliente);
            _banco.SaveChanges();
            throw new NotImplementedException();
        }

        public Cliente Login(string Email, string Senha)
        {
            Cliente cliente = _banco.Clientes.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
            return cliente;
            throw new NotImplementedException();
        }

        public Cliente ObterCliente(int id)
        {
            _banco.Clientes.Find(id);
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return _banco.Clientes.ToList();
            throw new NotImplementedException();
        }
    }
}
