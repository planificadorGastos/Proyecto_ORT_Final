using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
        [Display(Name = "Descripción")]
        public String Descripcion { get; set; }
        public Cuenta cuenta { get; set; }
        
        [Display(Name = "Fecha")]

        [Column(TypeName = "datetime2")]
        public DateTime Fecha { get; set; }
      
        
        public Usuario Usuario { get; set; }
    }
}