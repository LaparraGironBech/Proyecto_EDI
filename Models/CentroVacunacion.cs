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

            //Dar tamaño a la tabla hash, para cambiar tamaño hay que aumentar  las iteracions del for------------->
            for (int i = 0; i < 20; i++)
            {
                tablaPaciente.AgregarFinalLista();
            }
        }

        public void insertarPaciente(Paciente pac, PacienteIndice pac1, int prioridad)//Aqui se insertan en todas las estructuras a utilizar
        {
            listaPacientes.AgregarInicio(pac);
            avlPaciente.Insertar(pac1);
            priodadPaciente.insertar(prioridad, pac);
            tablaPaciente.Pos(FHash(pac.nombre + pac.apellido)).Agregar(FHash(pac.nombre + pac.apellido), pac);
            totalPacientes++;
            pacientesPrioridad++;
        }

        //Funsión Hash-------------------------->
        public int FHash(string titulo)// Nuestra función hash
        {

            titulo = titulo.ToLower(); //convertir todo a minuscula 
            int CHash = 0; //devolverá el valor en número
            char letra; // detecta letra por letra de la cadena
            for (int i = 0; i < titulo.Length; i++)
            {
                letra = Convert.ToChar(titulo.Substring(i, 1));
                CHash = CHash + Convert.ToInt32(letra);

            }

            CHash = CHash % 10;
            return CHash;
        }
        public void ExtraerPrioridad()
        {
            priodadPaciente.ExtraerInicio();
            pacientesPrioridad--;
        }
        public void ReinsertarPrioridad(int prioridad, Paciente pac)
        {
            priodadPaciente.insertar(prioridad, pac);
        }
    }
}
