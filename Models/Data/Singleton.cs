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
        public Lista<Paciente> listaPacientes;
        public Lista<Paciente> listaDeEspera;

        public int idSimulacro;
        public int Municipio;

        public List<Paciente> pacientebusqueda;

       public int id; //ID para actualizar datos


        private Singleton()
        {            
            PacienteList = new List<Paciente>();
            listaCentrosVacunacion = new Lista<CentroVacunacion>();
            listaReferencia = new Lista<int>();
            listaGeneralDePacientes = new Lista<Paciente>();
            listaPacientesVacunados = new Lista<Paciente>();
            pacientebusqueda = new List<Paciente>();
            posicion = 0;
            cantidadCentros = 0;
            listSimulacion = new Lista<PrioridadIndice>();
            listaPacientes = new Lista<Paciente>();
            listaDeEspera = new Lista<Paciente>();
            idSimulacro = 0;
            Municipio = 0;
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
