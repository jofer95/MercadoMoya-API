using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API{
    public class AzureUsuariosRepository : IUsuarios{
        public string azureConStr;
        public AzureUsuariosRepository()
        {
            azureConStr = "DefaultEndpointsProtocol=https;AccountName=s100ne2g2;AccountKey=IRr62TEFQHy4pnCbZypRae+rSD3e3kD7qE1WtqgMX7b6X2/UlVfKLGNnDkLDrDH+R/0tyiAJmfhbJZfDICZlUg==";
        }

        public async Task<bool> borrarUsuario(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<UsuarioAzureEntity> query = new TableQuery<UsuarioAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Articulo>();
            foreach (UsuarioAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                var upOp = TableOperation.Delete(az);
                await tabla.ExecuteAsync(upOp);
                return true;
            }
            return false;
        }

        public async Task<UsuarioAzureEntity> crearActualizarUsuario(Usuario obj)
        {
            var table = TablaAzure();
            // Create the table if it doesn't exist.
            var creada = await table.CreateIfNotExistsAsync();

            if (string.IsNullOrEmpty(obj.UsuarioID))
            {
                var azEn = new UsuarioAzureEntity();
                azEn.Nombre = obj.Nombre;
                azEn.Direccion = obj.Direccion;
                azEn.Contrasena = obj.Contrasena;
                azEn.Correo = obj.Correo;
                azEn.Rol = obj.Rol;
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(azEn);

                // Execute the insert operation.
                var x = await table.ExecuteAsync(insertOperation);
                return azEn;

            }
            else
            {
                TableQuery<UsuarioAzureEntity> query = new TableQuery<UsuarioAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.UsuarioID));

                var token = new TableContinuationToken();
                foreach (UsuarioAzureEntity az in await table.ExecuteQuerySegmentedAsync(query, token))
                {
                    az.Nombre = obj.Nombre;
                    az.Correo = obj.Correo;
                    az.Direccion = obj.Direccion;
                    az.Contrasena = obj.Contrasena;
                    az.Rol = obj.Rol;
                    var upOp = TableOperation.Replace(az);
                    await table.ExecuteAsync(upOp);
                    return az;
                }
                return null;
            }
        }

        public async Task<List<Usuario>> obtenerTodosLosUsuarios()
        {
            var table = TablaAzure();
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<UsuarioAzureEntity> query = new TableQuery<UsuarioAzureEntity>();
            //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            var token = new TableContinuationToken();
            var list = new List<Usuario>();
            // Print the fields for each customer.
            foreach (UsuarioAzureEntity entity in await table.ExecuteQuerySegmentedAsync(query, token))
            {
                list.Add(new Usuario()
                {
                    UsuarioID = entity.UsuarioID,
                    Nombre = entity.Nombre,
                    Contrasena = entity.Contrasena,
                    Correo = entity.Correo,
                    Direccion = entity.Direccion,
                    Rol = entity.Rol
                });
            }
            return list;
        }

        public async Task<Usuario> obtenerUsuarioPorCorreo(string correo)
        {
            var tabla = TablaAzure();
            var particion = correo;
            TableQuery<UsuarioAzureEntity> query = new TableQuery<UsuarioAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("Correo", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Usuario>();
            foreach (UsuarioAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                return new Usuario()
                {
                    UsuarioID = az.UsuarioID,
                    Correo = az.Correo,
                    Direccion = az.Direccion,
                    Contrasena = az.Contrasena,
                    Nombre = az.Nombre,
                    Rol = az.Rol
                };
            }
            return new Usuario();
        }

        public async Task<Usuario> obtenerUsuarioPorID(string id)
        {
            var tabla = TablaAzure();
            var particion = id;
            TableQuery<UsuarioAzureEntity> query = new TableQuery<UsuarioAzureEntity>()
            .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, particion));

            var token = new TableContinuationToken();
            var list = new List<Usuario>();
            foreach (UsuarioAzureEntity az in await tabla.ExecuteQuerySegmentedAsync(query, token))
            {
                return new Usuario()
                {
                    UsuarioID = az.UsuarioID,
                    Nombre = az.Nombre,
                    Correo = az.Correo,
                    Direccion = az.Direccion,
                    Contrasena = az.Contrasena,
                    Rol = az.Rol
                };
            }
            return null;
        }

        private CloudTable TablaAzure()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureConStr);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("MercadoMoyaUsuarios");
            return table;
        }
        
    }
}