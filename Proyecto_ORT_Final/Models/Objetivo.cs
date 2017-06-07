using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Objetivo
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public Decimal Monto { get; set; }
        public List<Notificacion> notificaciones { get; set; }
        public Usuario Usuario { get; set; }

    }
}