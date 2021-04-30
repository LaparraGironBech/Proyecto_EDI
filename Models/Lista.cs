using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class Lista<T>
    {
        public class Nodo
        {
            public Nodo(T t)
            {
                Siguiente = null;
                data = t;
            }
            private Nodo Siguiente;

            public Nodo siguiente
            {
                get { return Siguiente; }
                set { Siguiente = value; }
            }

            private T data;
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }
        //Declaraci´´on de los nodos y cantidades
        public int Cantidad;
        public Nodo PrimerNodo;
        public Nodo UltimoNodo;



        //Metodos
        public void AgregarInicio(T t)
        {
            Nodo Nom1 = new Nodo(t);
            if (Cantidad == 0)
            {
                PrimerNodo = Nom1;
                UltimoNodo = Nom1;
            }
            else
            {
                Nodo temp = Nom1;
                temp.siguiente = PrimerNodo;
                PrimerNodo = temp;

            }
            Cantidad++;
        }

        public void AgregarFinal(T t)
        {
            Nodo Nom2 = new Nodo(t);
            if (Cantidad == 0)
            {
                AgregarInicio(t);
            }
            else
            {
                UltimoNodo.siguiente = Nom2;
                UltimoNodo = Nom2;
            }
            Cantidad++;
        }

        public void AgregarPos(int pos, T t)
        {
            Nodo Nom3 = new Nodo(t);
            if (Cantidad == 0 || pos == 0)
            {
                AgregarInicio(t);
            }
            else
            {
                Nodo pretemp = PrimerNodo;
                Nodo temp = Nom3;
                int cont = 0;

                while (cont < (pos - 1))
                {
                    pretemp = pretemp.siguiente;
                    cont++;
                }
                temp.siguiente = pretemp.siguiente;
                pretemp.siguiente = temp;
                Cantidad++;
            }
        }


        public Nodo ObtenerInicio()
        {
            return PrimerNodo;
        }

        public Nodo ObtenerFinal()
        {
            return UltimoNodo;
        }

        public Nodo ObtenerPos(int Pos)
        {
            if (Cantidad == 0 || Pos == 0)
            {
                return ObtenerInicio();
            }
            else
                if (Pos >= Cantidad)
            {
                return ObtenerFinal();
            }
            else
            {
                Nodo temp = PrimerNodo;
                int cont = 0;
                while (cont < Pos)
                {
                    temp = temp.siguiente;
                    cont++;
                }
                return temp;
            }


        }

    }
}
