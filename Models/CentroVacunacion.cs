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

        public CentroVacunacion()
        {
            listaPacientes = new Lista<Paciente>(); ;
            tablaPaciente = new TablaHash<int, Paciente>();
            avlPaciente = new AVL<PacienteIndice>();
            priodadPaciente = new ColaDePrioridad();
        }

        public void insertarPaciente(Paciente pac, PacienteIndice pac1, int prioridad)
        {
            listaPacientes.AgregarFinal(pac);
            avlPaciente.Insertar(pac1);
            priodadPaciente.insertar(prioridad, pac);
        }        
    }
}
