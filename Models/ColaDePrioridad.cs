using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class ColaDePrioridad
    {         
        public int prioridad { get; set; }
        public Lista<PrioridadIndice> pacPrioridad;
        List<int> listaAlternativa;
        public int tamLista { get; set; }
        ColaDePrioridad()
        {
            tamLista = 0;
            pacPrioridad = null;
            listaAlternativa = null;

        }

        public void insertar(int prio, Paciente pac)
        {
            PrioridadIndice newTemp = new PrioridadIndice(prio, pac);
            if (tamLista == 0)
            {
                pacPrioridad.AgregarInicio(newTemp);
                listaAlternativa.Add(prio);
                tamLista++;
            }
            else
            {
                int aux = prio;
                for (int i = 0; i < tamLista; i++)
                {
                    if (aux < listaAlternativa[i])
                    {
                        if (i == tamLista-1)
                        {
                            aux = listaAlternativa[i];
                            listaAlternativa[tamLista] = aux;                            
                            pacPrioridad.AgregarFinal(pacPrioridad.DevolverValue(i));

                            listaAlternativa.Insert(i, prio);
                            intercambiar();
                        }
                        else
                        {
                            listaAlternativa.Insert(i, aux);
                            pacPrioridad.AgregarFinal(newTemp);
                        }
                    }
                }
            }
        }

        public void intercambiar()
        {

        }
    }
}
