using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolloVerde.ViewModels
{
    public class FacturaVM
    {
        public int idcliente { get; set; }
        public int idsucursal { get; set; }
        public int idtipopago { get; set; }
        public List<Items> items { get; set; }
        public int descuento { get; set; }
    }


    public class Items
    {
        public int id_producto { get; set; }
        public string descripcion_producto { get; set; }
        public int cantidad { get; set; }
    }
}