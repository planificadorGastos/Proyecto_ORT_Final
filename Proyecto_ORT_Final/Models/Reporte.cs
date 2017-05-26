using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Reporte
    {
        public int Id { get; set; }
        public Mapa mapa { get; set; }

        public Mapa Mapa
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}