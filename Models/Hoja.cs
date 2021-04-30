using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class Hoja<T>
    {
        public T value { get; set; }
        public Hoja<T> hojaIzquierda { get; set; }
        public Hoja<T> hojaDerecha { get; set; }
        public int altura { get; set; }
        public int FE { get; set; }

        public Hoja()
        {
            hojaIzquierda = null;
            hojaDerecha = null;
            this.FE = 0;
            this.altura = 0;            
        }
    }
}
