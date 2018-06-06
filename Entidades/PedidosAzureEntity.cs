using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace MercadoMoya_API{
    public class PedidosAzureEntity : TableEntity{
        public PedidosAzureEntity()
        {
            PartitionKey = "1";
            RowKey = RowKeyFromFecha();
        }

        public static string PartitionFromRowID(string id)
        {            
            return id;
        }
        public static string RowKeyFromFecha() => DateTime.Now.ToString("yyyMMddhh_mm:ss");
        public string PedidoID { get{ return RowKey; } }
        public string Usuario { get; set; }
        public string TempID { get; set; }
        public string Articulos { get; set; }
        public DateTime Fecha { get; set; }
        public bool Pagado { get; set; }
        public string EstatusPedido { get; set; }
        public double Total { get; set; }
        public string ReferenciaPago { get; set; }
    }
}