using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API{
    public class UsuarioAzureEntity : TableEntity{
        public UsuarioAzureEntity()
        {
            PartitionKey = "1";
            RowKey = RowKeyFromFecha();
        }

        public static string PartitionFromRowID(string id)
        {            
            return id;
        }
        public static string RowKeyFromFecha() => DateTime.Now.ToString("yyyMMddhh_mm:ss");

        
        public string UsuarioID { get { return RowKey; }}
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Direccion { get; set; }
        public int Rol { get; set; }
    }
}