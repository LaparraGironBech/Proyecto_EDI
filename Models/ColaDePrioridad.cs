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
        public ColaDePrioridad()
        {
            tamLista = 0;
            pacPrioridad = new Lista<PrioridadIndice>();
            listaAlternativa = new List<int>();

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
                PrioridadIndice auxPac = newTemp;
                for (int i = 0; i < tamLista; i++)
                {
                    if (aux < listaAlternativa[i])
                    {
                        //Cambio lista c#
                        int temp = aux;
                        aux = listaAlternativa[i];
                        listaAlternativa[i] = temp;
                        listaAlternativa.Add(aux);

                        //Cambio lista artesanal
                        PrioridadIndice tempPac = auxPac;
                        auxPac = pacPrioridad.DevolverValue(i);
                        pacPrioridad.insertarpos(i, tempPac);
                        pacPrioridad.AgregarFinal(auxPac);


                    }
                }
                tamLista++;
            }
        }

        public void ExtraerInicio()
        {
            listaAlternativa.RemoveAt(0);
            pacPrioridad.EliminarInicio();
        }       
    }
}
