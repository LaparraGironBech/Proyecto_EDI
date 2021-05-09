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
                return dpi.CompareTo(value.dpi);            
        }      
        public string dpi { get; set; }      
       
        public PacienteIndice(string DPI)
        {            
            dpi = DPI;
        }


    }
}
