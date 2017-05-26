using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public String Mail { get; set; }

        private Ranking ranking { get; set; }
        public List<Cuenta> cuentas { get; set; }

      
        }
    }
