using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MercadoMoya_API{
    [Produces("application/json")]
    [Route("api/Categorias/[action]")]
    public class CategoriaController : Controller{
        ICategorias categorias;
        public CategoriaController(){
            categorias = new AzureCategoriasRepository();
        }

        [HttpPost]
        [ActionName("ObtenerTodasLasCategorias")]
        public async Task<IActionResult> obtenerTodasLasCategorias()
        {
            var resultados = await categorias.obtenerTodasLasCategorias();
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerCategoriaPorID")]
        public async Task<IActionResult> ObtenerCategoriaPorID([FromBody]Categoria obj)
        {
            var resultados = await categorias.obtenerCategoriaPorID(obj.CategoriaID);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("CrearActualizarCategoria")]
        public async Task<IActionResult> CrearActualizarCategoria([FromBody]Categoria obj)
        {
            var resultados = await categorias.crearActualizarCategoria(obj);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("BorrarCategoria")]
        public async Task<IActionResult> BorrarCategoria(string id)
        {
            var resultados = await categorias.borrarCategoria(id);
            return Ok(resultados);
        }

    }
}