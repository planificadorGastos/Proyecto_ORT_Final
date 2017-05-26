using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    public class Objetivo
    {
        public int Id { get; set; }
        public List<Notificacion> notificaciones { get; set; }

        public Notificacion Notificacion
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