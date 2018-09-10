using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : RepositoryBase<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }
    }
}
