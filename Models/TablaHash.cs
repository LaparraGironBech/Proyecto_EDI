using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class TablaHash<K, V> where K : IComparable//Cada objeto de la tabla hash será una lista esto para cuando hayan colisiones
    {
        //colisiones  son los objetos contenidos en cada casilla hash
        public int cantidadcolisiones; //Cuantas colisiones hay
        public ObjetoHash<K, V> PrimerColision; //guarda la primera colision hash de la lista
        public ObjetoHash<K, V> UltimaColision;

        public int cantidadhash;
        public TablaHash<K, V> Primerhash;
        public TablaHash<K, V> Ultimohash;
        public TablaHash<K, V> Siguiente;

        public TablaHash() //método constructor
        {
            Siguiente = null;
        }


        //Métodos para agregar a la lista----------------------------------------------->
        public void agregarcolisioninicio(ObjetoHash<K, V> Nom1)//agrega al inicio de la lista
        {

            if (cantidadcolisiones == 0)
            {
                PrimerColision = Nom1;
                UltimaColision = Nom1;
                cantidadcolisiones++;
            }
            else
            {
                ObjetoHash<K, V> temp = Nom1;
                temp.siguiente = PrimerColision;
                PrimerColision = temp;
                cantidadcolisiones++;

            }

        }

        public void Agregar(K key, V value) // agrega al final de la lista
        {
            ObjetoHash<K, V> Nom2 = new ObjetoHash<K, V>(key, value);
            if (cantidadcolisiones == 0)
            {
                agregarcolisioninicio(Nom2);
            }
            else
            {
                UltimaColision.siguiente = Nom2;
                UltimaColision = Nom2;
                cantidadcolisiones++;
            }

        }
        //Métodos para obtener objeto de la lista------------------------------------------------->
        public TablaHash<K, V> InicioLista()
        {
            return Primerhash;
        }

        public TablaHash<K, V> FinalLista()
        {
            return Ultimohash;
        }

        public TablaHash<K, V> Pos(int pos)
        {
            if (cantidadhash == 0 || pos == 0)
            {
                return InicioLista();
            }
            else
                if (pos >= cantidadhash)
            {
                return FinalLista();
            }
            else
            {
                TablaHash<K, V> temp = Primerhash;
                int cont = 0;
                while (cont < pos)
                {
                    temp = temp.Siguiente;
                    cont++;
                }
                return temp;
            }
        }

        //Métodos para obtener objeto de la lista------------------------------------------------->
        public ObjetoHash<K, V> ObtenerInicio()
        {
            return PrimerColision;
        }

        public ObjetoHash<K, V> ObtenerFinal()
        {
            return UltimaColision;
        }

        public ObjetoHash<K, V> ObtenerPos(int Pos)
        {
            if (cantidadcolisiones == 0 || Pos == 0)
            {
                return ObtenerInicio();
            }
            else
                if (Pos >= cantidadcolisiones)
            {
                return ObtenerFinal();
            }
            else
            {
                ObjetoHash<K, V> temp = PrimerColision;
                int cont = 0;
                while (cont < Pos)
                {
                    temp = temp.siguiente;
                    cont++;
                }
                return temp;
            }
        }

        //Para las colisiones----------------------------------------------------------------------------->
        public V BuscarObjeto(K key) //Búsqueda por key se hara así, pues las colisiones se hará una búsqueda secuencial
        {
            ObjetoHash<K, V> Valor = new ObjetoHash<K, V>();
            int cont = 0;
            if (cantidadcolisiones > 0)
            {
                for (int i = 0; i < cantidadcolisiones; i++)
                {
                    if (key.CompareTo(ObtenerPos(cont).Key) == 0)
                    {
                        Valor = ObtenerPos(cont);
                        return Valor.Value;
                    }
                    cont++;
                }
                return Valor.Value;
            }
            else
            {
                return Valor.Value;
            }
        }

        public int BuscarPos(K key) //Búsqueda por key se hara así, pues las colisiones se hará una búsqueda secuencial
        {
            ObjetoHash<K, V> Valor = new ObjetoHash<K, V>();
            int cont = 0;
            int posi = -1;
            if (cantidadcolisiones > 0)
            {
                for (int i = 0; i < cantidadcolisiones; i++)
                {
                    if (key.CompareTo(ObtenerPos(cont).Key) == 0)
                    {
                        Valor = ObtenerPos(cont);
                        posi = cont;
                        return posi;
                    }
                    cont++;
                }
                return posi;
            }
            else
            {
                return posi;
            }
        }
        //Ver si hay valor repetido
        public bool Existe(K key) //verifica si ya existe un titulo
        {
            bool existe = false;
            int cont = 0;
            if (cantidadcolisiones > 0)
            {
                for (int i = 0; i < cantidadcolisiones; i++)
                {
                    if (key.CompareTo(ObtenerPos(cont).Key) == 0)
                    {
                        existe = true;
                        return existe;
                    }
                    cont++;
                }
                return existe;
            }
            else
            {
                return existe;
            }
        }

        //Metodos para eliminar-------------------------------------------------------------------------------------------->

        public void EliminarColisionInicio()
        {
            ObjetoHash<K, V> auxiliar = PrimerColision.siguiente;
            PrimerColision = auxiliar;
            cantidadcolisiones--;
        }

        public void EliminarColisionFinal()
        {
            if (cantidadcolisiones > 0)
            {
                ObjetoHash<K, V> auxiliar;
                auxiliar = ObtenerPos(cantidadcolisiones - 2);
                UltimaColision = auxiliar;
                UltimaColision.siguiente = null;
                cantidadcolisiones--;
            }
        }


        public void EliminarColision(K key)
        {
            int pos = BuscarPos(key);
            if (pos == 0)
            {
                EliminarColisionInicio();
            }
            else
            {
                if (pos == (cantidadcolisiones - 1))
                {
                    EliminarColisionFinal();
                }
                else
                {
                    if (pos > 0 && pos < (cantidadcolisiones - 1))
                    {
                        ObjetoHash<K, V> auxiliar1 = ObtenerPos(pos - 1);
                        ObjetoHash<K, V> auxiliar2 = ObtenerPos(pos);
                        ObjetoHash<K, V> auxiliar3 = ObtenerPos(pos + 1);

                        auxiliar1.siguiente = auxiliar3;
                        auxiliar2.siguiente = null;
                        cantidadcolisiones--;
                    }
                }
            }


        }

        //metodos para crear lista de lista
        public void AgregarInicioLista()
        {
            TablaHash<K, V> Nom1 = new TablaHash<K, V>();

            if (cantidadhash == 0)
            {
                Primerhash = Nom1;
                Ultimohash = Nom1;
                cantidadhash++;
            }
            else
            {
                TablaHash<K, V> temp = Nom1;
                temp.Siguiente = Primerhash;
                Primerhash = temp;
                cantidadhash++;

            }

        }
        public void AgregarFinalLista()//Solamente va a servir para agregar nueva casilla hash
        {
            TablaHash<K, V> Nom2 = new TablaHash<K, V>();
            if (cantidadhash == 0)
            {
                AgregarInicioLista();
            }
            else
            {
                Ultimohash.Siguiente = Nom2;
                Ultimohash = Nom2;
                cantidadhash++;
            }

        }
    }
}
