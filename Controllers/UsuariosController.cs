using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MercadoMoya_API
{
    [Produces("application/json")]
    [Route("api/Usuarios/[action]")]
    public class UsuariosController : Controller
    {
        IUsuarios usuarios;
        public UsuariosController()
        {
            usuarios = new AzureUsuariosRepository();
        }

        [HttpPost]
        [ActionName("ObtenerTodosLosUsuarios")]
        public async Task<IActionResult> ObtenerTodosLosArticulos()
        {
            var resultados = await usuarios.obtenerTodosLosUsuarios();
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerUsuarioPorID")]
        public async Task<IActionResult> ObtenerArticuloPorID([FromBody]Articulo obj)
        {
            var resultados = await usuarios.obtenerUsuarioPorID(obj.ArticuloID);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("CrearActualizarUsuario")]
        public async Task<IActionResult> CrearActualizarArticulo([FromBody]Usuario obj)
        {
            var resultados = await usuarios.crearActualizarUsuario(obj);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("BorrarArticulo")]
        public async Task<IActionResult> BorrarArticulo(string id)
        {
            var resultados = await usuarios.borrarUsuario(id);
            return Ok(resultados);
        }

        [HttpPost]
        [ActionName("ObtenerUsuarioPorCorreo")]
        public async Task<IActionResult> ObtenerUsuarioPorCorreo([FromBody]Usuario correo)
        {
            var resultados = await usuarios.obtenerUsuarioPorCorreo(correo.Correo);
            return Ok(resultados);
        }

    }
}