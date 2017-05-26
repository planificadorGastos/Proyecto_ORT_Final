using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }
        public String descripcion { get; set; }
        public decimal monto { get; set; }
        public Mapa mapa { get; set; }
        public Boolean pago { get; set; }
        public Cuenta cuenta { get; set; }
        public Usuario Usuario { get; set; }
        
    }
}