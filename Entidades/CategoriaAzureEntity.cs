using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API{
    public class CategoriaAzureEntity : TableEntity
    {
        public CategoriaAzureEntity()
        {
            PartitionKey = "1";
            RowKey = RowKeyFromFecha();
        }

        public static string PartitionFromRowID(string id)
        {            
            return id;
        }
        public static string RowKeyFromFecha() => DateTime.Now.ToString("yyyMMddhh_mm:ss");

        
        public string CategoriaID { get { return RowKey; }}
        public string Nombre { get; set; }
        public string ImagenURL { get; set; }
    }
}