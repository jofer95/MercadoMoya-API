using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API
{
    public class AzureArticulosRepository : IArticulos
    {
        public string azureConStr;

        public AzureArticulosRepository()
        {
            azureConStr = "DefaultEndpointsProtocol=https;AccountName=s100ne2g2;AccountKey=IRr62TEFQHy4pnCbZypRae+rSD3e3kD7qE1WtqgMX7b6X2/UlVfKLGNnDkLDrDH+R/0tyiAJmfhbJZfDICZlUg==";
        }

        private CloudTable TablaAzure()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureConStr);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("MercadoMoyaArticulos");
            return table;
        }

        public async Task<List<Articulo>> obtenerTodosLosArticulos()
        {
            var table = TablaAzure();
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<ArticulosAzureEntity> query = new TableQuery<ArticulosAzureEntity>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            var token = new TableContinuationToken();
            var list = new List<Articulo>();
            // Print the fields for each customer.
            foreach (ArticulosAzureEntity entity in await table.ExecuteQuerySegmentedAsync(query, token))
            {
                list.Add(new Articulo()
                {
                    ArticuloID = entity.ArticuloID,
                    Nombre = entity.Nombre,
                    Precio = entity.Precio,
                    Descripcion = entity.Descripcion,
                    Categoria = entity.Categoria,
                    ImagenURL = entity.ImagenURL
                });
            }
            return list;
        }

        public async Task<ArticulosAzureEntity> crearActualizarArticulo(Articulo obj)
        {
            var table = TablaAzure();
            // Create the table if it doesn't exist.
            var creada = await table.CreateIfNotExistsAsync();

            if (string.IsNullOrEmpty(obj.ArticuloID))
            {
                var azEn = new ArticulosAzureEntity();
                azEn.Nombre = obj.Nombre;
                azEn.Precio = obj.Precio;
                azEn.Descripcion = obj.Descripcion;
                azEn.Categoria = obj.Categoria;
                azEn.ImagenURL = obj.ImagenURL;
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(azEn);

                // Execute the insert operation.
                var x = await table.ExecuteAsync(insertOperation);
                return azEn;

            }
            else
            {
                TableQuery<ArticulosAzureEntity> query = new TableQuery<ArticulosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.ArticuloID));

                var token = new TableContinuationToken();
                foreach (ArticulosAzureEntity az in await table.ExecuteQuerySegmentedAsync(query, token))
                {
                    az.Nombre = obj.Nombre;
                    az.Descripcion = obj.Descripcion;
                    az.Precio = obj.Precio;
                    az.Categoria = obj.Categoria;
                    az.ImagenURL = obj.ImagenURL;
                    var upOp = TableOperation.Replace(az);
                    await table.ExecuteAsync(upOp);
                    return az;
                }
                return null;
            }
        }

        public async Task<Articulo> obtenerArticuloPorID(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<ArticulosAzureEntity> query = new TableQuery<ArticulosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Articulo>();
            foreach (ArticulosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                return new Articulo()
                {
                    ArticuloID = az.ArticuloID,
                    Nombre = az.Nombre,
                    Precio = az.Precio,
                    Categoria = az.Categoria,
                    Descripcion = az.Descripcion,
                    ImagenURL = az.ImagenURL
                };
            }
            return null;
        }

        public async Task<bool> borrarArticulo(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<ArticulosAzureEntity> query = new TableQuery<ArticulosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Usuario>();
            foreach (ArticulosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                var upOp = TableOperation.Delete(az);
                await tabla.ExecuteAsync(upOp);
                return true;
            }
            return false;
        }

        public async Task<List<Articulo>> obtenerArticulosPorID(List<string> artiuclos)
        {
            var tabla = TablaAzure();
            var particion = artiuclos;
            TableQuery<ArticulosAzureEntity> query = new TableQuery<ArticulosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, "particion"));

            var token = new TableContinuationToken();
            var list = new List<Articulo>();
            foreach (ArticulosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                /*return new Articulo()
                {
                    ArticuloID = az.ArticuloID,
                    Nombre = az.Nombre,
                    Precio = az.Precio,
                    Categoria = az.Categoria,
                    Descripcion = az.Descripcion,
                    ImagenURL = az.ImagenURL
                };*/
            }
            return list;
        }
    }
}