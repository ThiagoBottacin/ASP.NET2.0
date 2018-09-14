using System;
using System.Linq;
using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor) : base(contexto)
        {
            _contextAccessor = contextAccessor;
        }

        public void AddItem(string codigo)
        {
            var produto = _contexto.Set<Produto>()
                .SingleOrDefault(p => p.Codigo == codigo);

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado!");
            }

            var pedido = GetPedido();

            var itemPedido = _contexto.Set<ItemPedido>()
                .SingleOrDefault(i => i.Produto.Codigo == codigo 
                    && i.Pedido.Id == pedido.Id);

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _contexto.Set<ItemPedido>().Add(itemPedido);

                _contexto.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = _dbSet
                    .Include(p => p.Itens)
                        .ThenInclude(i => i.Produto)
                    .SingleOrDefault(p => p.Id == pedidoId);

            if (pedido == null)
            {
                pedido = new Pedido();
                Add(pedido);
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        private int? GetPedidoId()
        {
            return _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }
    }
}