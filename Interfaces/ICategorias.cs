using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoMoya_API{
    public interface ICategorias
    {
        Task<List<Categoria>> obtenerTodasLasCategorias();
        Task<CategoriaAzureEntity> crearActualizarCategoria(Categoria obj);
        Task<Categoria> obtenerCategoriaPorID(string id);
        Task<bool> borrarCategoria(string id);
    }
}