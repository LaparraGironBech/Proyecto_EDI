using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class PrioridadIndice
    {
        public int prioridad { get; set; }
        public Paciente pacientePrioridad;
        public PrioridadIndice(int priority, Paciente pac)
        {
            prioridad = priority;
            pacientePrioridad = pac;
        }
    }
}
