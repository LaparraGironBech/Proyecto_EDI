using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
      
        public List<Paciente> PacienteList;
        public List<SelectListItem> SelectListItemList;
        private Singleton()
        {
            PacienteList = new List<Paciente>();
            SelectListItemList = new List<SelectListItem>();
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
