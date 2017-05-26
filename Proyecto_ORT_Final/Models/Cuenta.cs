using System;
using System.Collections.Generic;
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
        public String Nombre { get; set; }
        public Decimal SaldoInicial { get; set; }
        public Decimal SaldoRestante { get; set; }
        public String TipoMoneda { get; set; }
        public Usuario Usuario { get; set; }

        [NotMapped]
        public SelectList monedas { get; set; }

        [NotMapped]
        public string monedaPorDefecto { get; private set; }

        public Cuenta()
        {
            SaldoRestante = SaldoInicial ;
        }
        
    }
}