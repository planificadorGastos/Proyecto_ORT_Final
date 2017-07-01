using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }
        public String Qr { get; set; }
        public DateTime Fecha { get; set; }

    }
}