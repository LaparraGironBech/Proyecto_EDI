using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class AVL<T> where T : IComparable
    {
        public Hoja<T> raiz;
        public int cantidadHojas { get; set; }
        bool existe = false;
        public AVL()
        {
            raiz = null;
            existe = false;
        }
        public void Busqueda(Hoja<T> r, ref Hoja<T> medicamento, ref bool existe)
        {
            if (r.value.CompareTo(medicamento.value) == 0)
            {
                medicamento = r;
                existe = true;

            }
            else
            {
                if (r.hojaIzquierda != null)
                {
                    Busqueda(r.hojaIzquierda, ref medicamento, ref existe);
                }
                if (r.hojaDerecha != null)
                {
                    Busqueda(r.hojaDerecha, ref medicamento, ref existe);
                }
            }
        }
        public int obtenerFE(Hoja<T> x)
        {
            if (x == null)
            {
                return -1;
            }
            else
            {
                return x.altura;
            }
        }
        public Hoja<T> rotacionSimpIzquierda(Hoja<T> valor)
        {
            Hoja<T> aux = valor.hojaIzquierda;
            valor.hojaIzquierda = aux.hojaDerecha;
            aux.hojaDerecha = valor;
            valor.altura = Math.Max(obtenerFE(valor.hojaIzquierda), obtenerFE(valor.hojaDerecha)) + 1;
            aux.altura = Math.Max(obtenerFE(aux.hojaIzquierda), obtenerFE(aux.hojaDerecha)) + 1;
            return aux;
        }
        public Hoja<T> rotacionSimpDerecha(Hoja<T> valor)
        {
            Hoja<T> aux = valor.hojaDerecha;
            valor.hojaDerecha = aux.hojaIzquierda;
            aux.hojaIzquierda = valor;
            valor.altura = Math.Max(obtenerFE(valor.hojaIzquierda), obtenerFE(valor.hojaDerecha)) + 1;
            aux.altura = Math.Max(obtenerFE(aux.hojaIzquierda), obtenerFE(aux.hojaDerecha)) + 1;
            return aux;
        }

        public Hoja<T> rotacionDobIzquierda(Hoja<T> valor)
        {
            Hoja<T> aux;
            valor.hojaIzquierda = rotacionSimpDerecha(valor.hojaIzquierda);
            aux = rotacionSimpIzquierda(valor);
            return aux;
        }

        public Hoja<T> rotacionDobDerecha(Hoja<T> valor)
        {
            Hoja<T> aux;
            valor.hojaDerecha = rotacionSimpIzquierda(valor.hojaDerecha);
            aux = rotacionSimpDerecha(valor);
            return aux;
        }

        public void Insertar(T value)
        {
            cantidadHojas++;
            Hoja<T> nuevaHoja = new Hoja<T>();
            nuevaHoja.value = value;
            nuevaHoja.hojaIzquierda = null;
            nuevaHoja.hojaDerecha = null;
            if (raiz == null)
            {
                raiz = nuevaHoja;
            }
            else
            {
                raiz = insertarAVL(nuevaHoja, raiz);
            }
        }

        public Hoja<T> insertarAVL(Hoja<T> nuevo, Hoja<T> ArbolSub)
        {
            Hoja<T> nuevoPadre = ArbolSub;
            if (nuevo.value.CompareTo(ArbolSub.value) < 0)
            {
                if (ArbolSub.hojaIzquierda == null)
                {
                    ArbolSub.hojaIzquierda = nuevo;
                }
                else
                {
                    ArbolSub.hojaIzquierda = insertarAVL(nuevo, ArbolSub.hojaIzquierda);
                    if ((obtenerFE(ArbolSub.hojaIzquierda) - obtenerFE(ArbolSub.hojaDerecha) == 2))
                    {
                        if (nuevo.value.CompareTo(ArbolSub.hojaIzquierda.value) < 0)
                        {
                            nuevoPadre = rotacionSimpIzquierda(ArbolSub);
                        }
                        else
                        {
                            nuevoPadre = rotacionDobIzquierda(ArbolSub);
                        }
                    }
                }
            }
            else if (nuevo.value.CompareTo(ArbolSub.value) > 0)
            {
                if (ArbolSub.hojaDerecha == null)
                {
                    ArbolSub.hojaDerecha = nuevo;
                }
                else
                {
                    ArbolSub.hojaDerecha = insertarAVL(nuevo, ArbolSub.hojaDerecha);
                    if ((obtenerFE(ArbolSub.hojaDerecha) - obtenerFE(ArbolSub.hojaIzquierda) == 2))
                    {
                        if (nuevo.value.CompareTo(ArbolSub.hojaDerecha.value) > 0)
                        {
                            nuevoPadre = rotacionSimpDerecha(ArbolSub);
                        }
                        else
                        {
                            nuevoPadre = rotacionDobDerecha(ArbolSub);
                        }
                    }
                }
            }
            else
            {
                //
            }

            if ((ArbolSub.hojaIzquierda == null) && (ArbolSub.hojaDerecha != null))
            {
                ArbolSub.altura = ArbolSub.hojaDerecha.altura + 1;
            }
            else if ((ArbolSub.hojaDerecha == null) && (ArbolSub.hojaIzquierda != null))
            {
                ArbolSub.altura = ArbolSub.hojaIzquierda.altura + 1;
            }
            else
            {
                ArbolSub.altura = Math.Max(obtenerFE(ArbolSub.hojaIzquierda), obtenerFE(ArbolSub.hojaDerecha)) + 1;
            }
            nuevoPadre.FE = (obtenerFE(ArbolSub.hojaIzquierda) - obtenerFE(ArbolSub.hojaDerecha));
            return nuevoPadre;
        }
    }
}
