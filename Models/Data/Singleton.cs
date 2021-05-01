using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Personas> PersonasList;
        private Singleton()
        {
            PersonasList = new List<Personas>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
