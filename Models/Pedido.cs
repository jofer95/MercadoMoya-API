using System;
using System.Collections.Generic;

namespace MercadoMoya_API{
    public class Pedido{
        public string PedidoID { get; set; }
        public string Usuario { get; set; }
        public string TempID { get; set; }
        public List<ArticulosPedido> Articulos { get; set; }
        public DateTime Fecha { get; set; }
        public bool Pagado { get; set; }
        public string EstatusPedido { get; set; }
        public double Total { get; set; }
        public string ReferenciaPago { get; set; }
    }
}