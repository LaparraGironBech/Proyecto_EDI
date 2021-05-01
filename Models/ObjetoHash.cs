using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class ObjetoHash<K, V> where K : IComparable  //identifica de que tipo será el key y de que tipo serra el valor
    {
        public int CompareTo(object? obj)
        {
            ObjetoHash<K, V> value = (ObjetoHash<K, V>)obj;
            return Key.CompareTo(value.Key);
        }

        public K Key { get; set; } //llave 
        public V Value { get; set; }//valor del objeto
        public ObjetoHash<K, V> siguiente { get; set; }

        public ObjetoHash(K KEY, V VALUE)
        {
            this.Key = KEY;
            this.Value = VALUE;
            siguiente = null;
        }
        public ObjetoHash()
        {
            siguiente = null;
        }
    }
}
