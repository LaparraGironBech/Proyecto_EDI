using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class PacienteIndiceApellido : IComparable
    {
        public int CompareTo(object? obj)
        {
            PacienteIndiceApellido value = (PacienteIndiceApellido)obj;
            return apellido.CompareTo(value.apellido);
        }
        public string apellido { get; set; }

        public PacienteIndiceApellido(string lastName)
        {
            apellido = lastName;
        }
    }
}
