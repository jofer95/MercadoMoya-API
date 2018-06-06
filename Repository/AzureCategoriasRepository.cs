using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API{
    public class AzureCategoriasRepository : ICategorias{
        public string azureConStr;

        public AzureCategoriasRepository()
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
            CloudTable table = tableClient.GetTableReference("MercadoMoyaCategorias");
            return table;
        }

        public async Task<bool> borrarCategoria(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<CategoriaAzureEntity> query = new TableQuery<CategoriaAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Categoria>();
            foreach (CategoriaAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                var upOp = TableOperation.Delete(az);
                await tabla.ExecuteAsync(upOp);
                return true;
            }
            return false;
        }

        public async Task<CategoriaAzureEntity> crearActualizarCategoria(Categoria obj)
        {
            var table = TablaAzure();
            // Create the table if it doesn't exist.
            var creada = await table.CreateIfNotExistsAsync();

            if (string.IsNullOrEmpty(obj.CategoriaID))
            {
                var azEn = new CategoriaAzureEntity();
                azEn.Nombre = obj.Nombre;
                azEn.ImagenURL = obj.ImagenURL;
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(azEn);

                // Execute the insert operation.
                var x = await table.ExecuteAsync(insertOperation);
                return azEn;

            }
            else
            {
                TableQuery<CategoriaAzureEntity> query = new TableQuery<CategoriaAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.CategoriaID));

                var token = new TableContinuationToken();
                foreach (CategoriaAzureEntity az in await table.ExecuteQuerySegmentedAsync(query, token))
                {
                    az.Nombre = obj.Nombre;
                    az.ImagenURL = obj.ImagenURL;
                    var upOp = TableOperation.Replace(az);
                    await table.ExecuteAsync(upOp);
                    return az;
                }
                return null;
            }
        }

        public async Task<Categoria> obtenerCategoriaPorID(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<CategoriaAzureEntity> query = new TableQuery<CategoriaAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Categoria>();
            foreach (CategoriaAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                return new Categoria()
                {
                    CategoriaID = az.CategoriaID,
                    Nombre = az.Nombre,
                    ImagenURL = az.ImagenURL
                };
            }
            return null;
        }

        public async Task<List<Categoria>> obtenerTodasLasCategorias()
        {
            var table = TablaAzure();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<CategoriaAzureEntity> query = new TableQuery<CategoriaAzureEntity>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            var token = new TableContinuationToken();
            var list = new List<Categoria>();
            // Print the fields for each customer.
            foreach (CategoriaAzureEntity entity in await table.ExecuteQuerySegmentedAsync(query, token))
            {
                list.Add(new Categoria()
                {
                    CategoriaID = entity.CategoriaID,
                    Nombre = entity.Nombre,
                    ImagenURL = entity.ImagenURL
                });
            }
            return list;
        }
    }
}