using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ORT_Final.Models
{
    public class Cuenta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        public String Nombre { get; set; }
        [Display(Name = "Saldo inicial")]
        public Decimal SaldoInicial { get; set; }
        [Display(Name = "Saldo restante")]
        public Decimal SaldoRestante { get; set; }
        [Display(Name = "Tipo de moneda")]
        public String TipoMoneda { get; set; }

        public Usuario Usuario { get; set; }

        public Cuenta()
        {
           
            SaldoRestante = SaldoInicial ;
        }
        
    }
}