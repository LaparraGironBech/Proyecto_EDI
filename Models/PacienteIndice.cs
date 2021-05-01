using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class PacienteIndice : IComparable
    {
        public int CompareTo(object? obj)
        {            
            PacienteIndice value = (PacienteIndice)obj;
            if (parametro == 0)
            {
                return nombre.CompareTo(value.nombre);
            }
            else if (parametro == 1)
            {
                return apellido.CompareTo(value.apellido);
            }
            else
            {
                return dpi.CompareTo(value.dpi);
            }
        }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dpi { get; set; }
        public int parametro { get; set; }

        public PacienteIndice(int PAR)
        {
            parametro = PAR;
        }


    }
}
