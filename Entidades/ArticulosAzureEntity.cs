using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API
{
    public class ArticulosAzureEntity : TableEntity
    {
        public ArticulosAzureEntity()
        {
            PartitionKey = "1";
            RowKey = RowKeyFromFecha();
        }

        public static string PartitionFromRowID(string id)
        {            
            return id;
        }
        public static string RowKeyFromFecha() => DateTime.Now.ToString("yyyMMddhh_mm:ss");

        
        public string ArticuloID { get { return RowKey; }}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Categoria { get; set; }
        public string ImagenURL { get; set; }
    }
}