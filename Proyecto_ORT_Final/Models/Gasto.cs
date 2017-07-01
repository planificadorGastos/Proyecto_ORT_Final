using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        [Display(Name = "Fecha")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "datetime2")]
        public DateTime fecha { get; set; }
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }
        [Display(Name = "Monto")]
        public decimal monto { get; set; }
        public Mapa mapa { get; set; }
        [Display(Name = "¿Pago?")]
        public Boolean pago { get; set; }
        public Cuenta cuenta { get; set; }
        [Display(Name = "Tipo de moneda")]
        public String TipoMoneda { get; set; }

        [Display(Name = "Imágen")]
        public Byte[] Imagen { get; set; }

        public Usuario Usuario { get; set; }

    }
}