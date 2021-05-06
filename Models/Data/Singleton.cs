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
        public Lista<CentroVacunacion> listaCentrosVacunacion;
        public Lista<int> listaReferencia;
        public int posicion;
        public int cantidadCentros;
        public Lista<Paciente> listaGeneralDePacientes;
        public Lista<Paciente> listaPacientesVacunados;
        public Lista<PrioridadIndice> listSimulacion;
        public Lista<Paciente> listaSimulacionS;


        private Singleton()
        {            
            PacienteList = new List<Paciente>();
            listaCentrosVacunacion = new Lista<CentroVacunacion>();
            listaReferencia = new Lista<int>();
            listaGeneralDePacientes = new Lista<Paciente>();
            listaPacientesVacunados = new Lista<Paciente>();
            posicion = 0;
            cantidadCentros = 0;
            listSimulacion = new Lista<PrioridadIndice>();
            listaSimulacionS = new Lista<Paciente>();
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
