using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class CentroVacunacion
    {
        public Lista<Paciente> listaPacientes;
        public TablaHash<int, Paciente> tablaPaciente;
        public AVL<PacienteIndice> avlPaciente;
        public ColaDePrioridad priodadPaciente;
        public int totalPacientes;
        public int pacientesPrioridad;

        public CentroVacunacion()
        {
            listaPacientes = new Lista<Paciente>(); ;
            tablaPaciente = new TablaHash<int, Paciente>();
            avlPaciente = new AVL<PacienteIndice>();
            priodadPaciente = new ColaDePrioridad();
            totalPacientes = 0;
            pacientesPrioridad = 0;
        }

        public void insertarPaciente(Paciente pac, PacienteIndice pac1, int prioridad)
        {
            listaPacientes.AgregarInicio(pac);
            avlPaciente.Insertar(pac1);
            priodadPaciente.insertar(prioridad, pac);
            totalPacientes++;
            pacientesPrioridad++;
        }
        public void ExtraerPaciente()
        {
            priodadPaciente.ExtraerInicio();
            pacientesPrioridad--;
        }        
    }
}
