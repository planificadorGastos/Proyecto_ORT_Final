using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Objetivo
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }
        [Column(TypeName = "datetime2")]
        
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoMensual { get; set; }

        public String TipoMoneda { get; set; }
        public Boolean Pago { get; set; }
        public int CuotaActual { get; set; }
        public int CuotasTotales { get; set; }
        public List<Notificacion> notificaciones { get; set; }
        public Usuario Usuario { get; set; }
        public Cuenta Cuenta { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FechaUltimaCuotaPaga { get; set; }
        public Boolean DebitoAutomatico { get; set; }
    }
}