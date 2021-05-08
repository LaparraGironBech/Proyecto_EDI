using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_EDI.Models.Data;

namespace Proyecto_EDI.Models
{
    public class SeriePastel
    {
        //name: 'Chrome',
        //    y: 61.41,
        //    sliced: true,
        //    selected: true

        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }

        public SeriePastel()
        {

        }

        public SeriePastel(string name, double y, bool sliced = false, bool selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }

        public List<SeriePastel> GetDataDummy()
        {
            List<SeriePastel> lista = new List<SeriePastel>();
            decimal total = (Singleton.Instance.listaPacientesVacunados.Cantidad * 100) / Singleton.Instance.listaGeneralDePacientes.Cantidad;
            Singleton.Instance.porcVac = decimal.Round(total, 2);
            var porcentajefinal = Convert.ToDouble(Singleton.Instance.porcVac);
            lista.Add(new SeriePastel("Vacunados", porcentajefinal));
            lista.Add(new SeriePastel("No Vacunados", 100- porcentajefinal));
            

            return lista;
        }
    }
}
