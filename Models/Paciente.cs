using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class Paciente
    {
        [Required]
        public int? dpi { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]

        public string departamento { get; set; }
        [Required]
        public string municipio { get; set; }
        [Required]
        public int edad { get; set; }
        [Required]
        public int grupo_prioridad { get; set; }
        [Required]
        public bool vacunado { get; set; }
        public Paciente()
        {
            nombre = "";
            apellido = "";
            dpi = 0;
            departamento = "";
            municipio = "";
            edad = 0;
            grupo_prioridad = 0;
            vacunado = false;
        }
    }
}
