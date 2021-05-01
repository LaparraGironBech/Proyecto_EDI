using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class Paciente
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dpi { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public int edad { get; set; }
        public int grupo_prioridad { get; set; }
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
