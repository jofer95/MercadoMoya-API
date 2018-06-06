using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoMoya_API{
    public interface IUsuarios
    {
        Task<List<Usuario>> obtenerTodosLosUsuarios();
        Task<UsuarioAzureEntity> crearActualizarUsuario(Usuario obj);
        Task<Usuario> obtenerUsuarioPorID(string id);
        Task<Usuario> obtenerUsuarioPorCorreo(string correo);
        Task<bool> borrarUsuario(string id);
    }
}