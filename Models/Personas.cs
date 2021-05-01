using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class Personas
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int Cui { get; set; }
        [Required]
        public string Departamento { get; set; }
        [Required]
        public string Municipio { get; set; }
        [Required]
        public string Trabajo { get; set; }

    }
}
