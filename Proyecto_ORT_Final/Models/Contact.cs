using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_ORT_Final.Models
{
    [Table("Contacts")]
    public class Contact
    {
        public int Id { get; set; }
        [StringLength(100),Required]
        public String Nombre { get; set; }
        [StringLength(100),Required]
        public String email { get; set; }
        [StringLength(100),Required]
        public String Telefono { get; set; }
        [StringLength(1000),Required]
        public String Mensaje { get; set; }

    }
}