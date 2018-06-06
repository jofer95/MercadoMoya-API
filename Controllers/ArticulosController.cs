using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MercadoMoya_API{

    [Produces("application/json")]
    [Route("api/Articulos/[action]")]
    public class ArticulosController : Controller{
        IArticulos articulos;
        public ArticulosController(){
            articulos = new AzureArticulosRepository();
        }

        [HttpPost]
        [ActionName("ObtenerTodosLosArticulos")]
        public async Task<IActionResult> ObtenerTodosLosArticulos()
        {
            var resultados = await articulos.obtenerTodosLosArticulos();
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerArticuloPorID")]
        public async Task<IActionResult> ObtenerArticuloPorID([FromBody]Articulo obj)
        {
            var resultados = await articulos.obtenerArticuloPorID(obj.ArticuloID);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("CrearActualizarArticulo")]
        public async Task<IActionResult> CrearActualizarArticulo([FromBody]Articulo obj)
        {
            var resultados = await articulos.crearActualizarArticulo(obj);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("BorrarArticulo")]
        public async Task<IActionResult> BorrarArticulo(string id)
        {
            var resultados = await articulos.borrarArticulo(id);
            return Ok(resultados);
        }

    }
}