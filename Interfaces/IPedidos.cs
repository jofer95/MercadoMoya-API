using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoMoya_API{
    public interface IPedidos
    {
        Task<List<Pedido>> obtenerTodosLosPedidos();
        Task<Pedido> crearActualizarPedido(Pedido obj);
        Task<Pedido> obtenerPedidoPorID(string id);
        Task<Pedido> obtenerPedidoPorUsuario(string id);
        Task<Pedido> obtenerPedidoPorIDTemp(string id);
        Task<PedidosAzureEntity> borrarPedido(string id);
    }
}