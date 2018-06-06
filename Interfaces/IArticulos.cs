using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoMoya_API{
    public interface IArticulos
    {
        Task<List<Articulo>> obtenerTodosLosArticulos();
        Task<List<Articulo>> obtenerArticulosPorID(List<string> artiuclos);
        Task<ArticulosAzureEntity> crearActualizarArticulo(Articulo obj);
        Task<Articulo> obtenerArticuloPorID(string id);
        Task<bool> borrarArticulo(string id);
    }
}