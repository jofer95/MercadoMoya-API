using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MercadoMoya_API{

    [Produces("application/json")]
    [Route("api/Pedidos/[action]")]
    public class PedidosController : Controller{
        IPedidos pedidos;
        public PedidosController(){
            pedidos = new AzurePedidosRepository();
        }

        [HttpPost]
        [ActionName("ObtenerTodosLosPedidos")]
        public async Task<IActionResult> ObtenerTodosLosPedidos()
        {
            var resultados = await pedidos.obtenerTodosLosPedidos();
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerPedidoPorID")]
        public async Task<IActionResult> ObtenerPedidoPorID([FromBody]Pedido obj)
        {
            var resultados = await pedidos.obtenerPedidoPorID(obj.PedidoID);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerPedidoPorUsuario")]
        public async Task<IActionResult> ObtenerPedidoPorUsuario([FromBody]Pedido obj)
        {
            var resultados = await pedidos.obtenerPedidoPorUsuario(obj.Usuario);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerPedidoPorIDTemp")]
        public async Task<IActionResult> ObtenerPedidoPorIDTemp([FromBody]Pedido obj)
        {
            var resultados = await pedidos.obtenerPedidoPorIDTemp(obj.TempID);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("CrearActualizarPedido")]
        public async Task<IActionResult> CrearActualizarPedido([FromBody]Pedido obj)
        {
            var resultados = await pedidos.crearActualizarPedido(obj);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("BorrarPedido")]
        public async Task<IActionResult> BorrarPedido([FromBody]Pedido obj)
        {
            var resultados = await pedidos.borrarPedido(obj.PedidoID);
            return Ok(resultados);
        }

    }
}