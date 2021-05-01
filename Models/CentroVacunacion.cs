using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDI.Models
{
    public class CentroVacunacion
    {
        Lista<Paciente> listaPacientes;
        TablaHash<int, Paciente> tablaPaciente;
        AVL<PacienteIndice> avlPaciente;
        ColaDePrioridad priodadPaciente;

        CentroVacunacion()
        {
            listaPacientes = null;
            tablaPaciente = null;
            avlPaciente = null;
            priodadPaciente = null;
        }
    }
}
