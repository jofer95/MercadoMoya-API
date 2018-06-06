using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace MercadoMoya_API
{
    public class AzurePedidosRepository : IPedidos
    {
        public string azureConStr;

        public AzurePedidosRepository()
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
            CloudTable table = tableClient.GetTableReference("MercadoMoyaPedidos");
            return table;
        }
        public async Task<PedidosAzureEntity> borrarPedido(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Pedido>();
            foreach (PedidosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                var upOp = TableOperation.Delete(az);
                await tabla.ExecuteAsync(upOp);
                return az;
            }
            return new PedidosAzureEntity();
        }

        public async Task<Pedido> crearActualizarPedido(Pedido obj)
        {
            var table = TablaAzure();
            // Create the table if it doesn't exist.
            var creada = await table.CreateIfNotExistsAsync();

            if (string.IsNullOrEmpty(obj.PedidoID))
            {
                var azEn = new PedidosAzureEntity();
                azEn.Usuario = obj.Usuario;
                string json = JsonConvert.SerializeObject(obj.Articulos);
                azEn.Articulos = json;
                azEn.TempID = obj.TempID;
                azEn.EstatusPedido = obj.EstatusPedido;
                azEn.Pagado = obj.Pagado;
                azEn.Fecha = DateTime.Now;
                azEn.Total = obj.Total;
                azEn.ReferenciaPago = obj.ReferenciaPago;
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(azEn);

                // Execute the insert operation.
                var x = await table.ExecuteAsync(insertOperation);
                obj.PedidoID = azEn.PedidoID;
                return obj;

            }
            else
            {
                TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.PedidoID));

                var token = new TableContinuationToken();
                foreach (PedidosAzureEntity az in await table.ExecuteQuerySegmentedAsync(query, token))
                {
                    az.Usuario = obj.Usuario;
                    string json = JsonConvert.SerializeObject(obj.Articulos);
                    az.Articulos = json;
                    az.TempID = obj.TempID;
                    az.EstatusPedido = obj.EstatusPedido;
                    az.Pagado = obj.Pagado;
                    az.Fecha = obj.Fecha;
                    az.Total = obj.Total;
                    az.ReferenciaPago = obj.ReferenciaPago;
                    var upOp = TableOperation.Replace(az);
                    await table.ExecuteAsync(upOp);
                    obj.PedidoID = az.PedidoID;
                    return obj;
                }
                return null;
            }
        }

        public async Task<Pedido> obtenerPedidoPorID(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Pedido>();
            foreach (PedidosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                List<ArticulosPedido> articulos = JsonConvert.DeserializeObject<List<ArticulosPedido>>(az.Articulos);
                return new Pedido()
                {
                    PedidoID = az.PedidoID,
                    Usuario = az.Usuario,
                    TempID = az.TempID,
                    Articulos = articulos,
                    Fecha = az.Fecha,
                    Pagado = az.Pagado,
                    EstatusPedido = az.EstatusPedido,
                    Total = az.Total,
                    ReferenciaPago = az.ReferenciaPago
                };
            }
            return new Pedido();
        }

        public async Task<Pedido> obtenerPedidoPorIDTemp(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("TempID", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Pedido>();
            foreach (PedidosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                List<ArticulosPedido> articulos = JsonConvert.DeserializeObject<List<ArticulosPedido>>(az.Articulos);
                return new Pedido()
                {
                    PedidoID = az.PedidoID,
                    Usuario = az.Usuario,
                    TempID = az.TempID,
                    Articulos = articulos,
                    Fecha = az.Fecha,
                    Pagado = az.Pagado,
                    EstatusPedido = az.EstatusPedido,
                    Total = az.Total,
                    ReferenciaPago = az.ReferenciaPago
                };
            }
            return new Pedido();
        }

        public async Task<Pedido> obtenerPedidoPorUsuario(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("Usuario", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Pedido>();
            foreach (PedidosAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                List<ArticulosPedido> articulos = JsonConvert.DeserializeObject<List<ArticulosPedido>>(az.Articulos);
                return new Pedido()
                {
                    PedidoID = az.PedidoID,
                    Usuario = az.Usuario,
                    TempID = az.TempID,
                    Articulos = articulos,
                    Fecha = az.Fecha,
                    Pagado = az.Pagado,
                    EstatusPedido = az.EstatusPedido,
                    Total = az.Total,
                    ReferenciaPago = az.ReferenciaPago
                };
            }
            return new Pedido();
        }

        public async Task<List<Pedido>> obtenerTodosLosPedidos()
        {
            var table = TablaAzure();
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<PedidosAzureEntity> query = new TableQuery<PedidosAzureEntity>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            var token = new TableContinuationToken();
            var list = new List<Pedido>();
            // Print the fields for each customer.
            foreach (PedidosAzureEntity entity in await table.ExecuteQuerySegmentedAsync(query, token))
            {
                List<ArticulosPedido> articulos = JsonConvert.DeserializeObject<List<ArticulosPedido>>(entity.Articulos);
                list.Add(new Pedido()
                {
                    PedidoID = entity.PedidoID,
                    Usuario = entity.Usuario,
                    TempID = entity.TempID,
                    Articulos = articulos,
                    Fecha = entity.Fecha,
                    Pagado = entity.Pagado,
                    EstatusPedido = entity.EstatusPedido,
                    Total = entity.Total,
                    ReferenciaPago = entity.ReferenciaPago
                });
            }
            return list;
        }
    }
}