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
        public int dpi { get; set; }
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
        public Paciente(string Name, string lastname, int DPI, string departament, string muni, int ed, int prio)
        {
            nombre = Name;
            apellido = lastname;
            dpi = DPI;
            departamento = departament;
            municipio = muni;
            edad = ed;
            grupo_prioridad = prio;
            vacunado = false;
        }
        public Paciente()
        {

        }
    }
}
