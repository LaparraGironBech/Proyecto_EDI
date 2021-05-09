using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class PacienteIndiceNombre : IComparable
    {
        public int CompareTo(object? obj)
        {
            PacienteIndiceNombre value = (PacienteIndiceNombre)obj;
            return nombre.CompareTo(value.nombre);
        }
        public string nombre { get; set; }

        public PacienteIndiceNombre(string Name)
        {
            nombre = Name;
        }
    }
}
