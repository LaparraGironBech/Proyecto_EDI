using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_EDI.Models.Data;
using Proyecto_EDI.Models;
using System.IO;
using Microsoft.AspNetCore.Routing;
namespace Proyecto_EDI.Controllers
{
    public class PacienteController : Controller
    {
        //Lista<PrioridadIndice> listSimulacion = new Lista<PrioridadIndice>();
        ////Lista<Paciente> listaSimulacionS = new Lista<Paciente>();
        // GET: PacienteController
        public ActionResult Index()
        {
            return View(Singleton.Instance.PacienteList);
        }

        public IActionResult search(string Buscar, string Busqueda)
        {



            Singleton.Instance.pacientebusqueda.Clear();
            int opcion = Convert.ToInt32(Buscar);

            switch (opcion)
            {
                case 0://Busqueda por nombre 
                    for (int i = 0; i < Singleton.Instance.listaPacientes.Cantidad; i++)
                    {
                        string nombre = Singleton.Instance.listaPacientes.ObtenerPos(i).Data.nombre;
                        if (nombre == Busqueda)
                        {
                            Paciente Buscado = Singleton.Instance.listaPacientes.ObtenerPos(i).Data;
                            Singleton.Instance.pacientebusqueda.Add(Buscado);
                        }
                    }
                    break;

                case 1://apellido
                    for (int i = 0; i < Singleton.Instance.listaPacientes.Cantidad; i++)
                    {
                        string apellido = Singleton.Instance.listaPacientes.ObtenerPos(i).Data.apellido;
                        if (apellido == Busqueda)
                        {
                            Paciente Buscado = Singleton.Instance.listaPacientes.ObtenerPos(i).Data;
                            Singleton.Instance.pacientebusqueda.Add(Buscado);
                        }
                    }
                    break;
                case 2://DPI
                    for (int i = 0; i < Singleton.Instance.listaPacientes.Cantidad; i++)
                    {
                        string DPI = Convert.ToString(Singleton.Instance.listaPacientes.ObtenerPos(i).Data.dpi);
                        if (DPI == Busqueda)
                        {
                            Paciente Buscado = Singleton.Instance.listaPacientes.ObtenerPos(i).Data;
                            Singleton.Instance.pacientebusqueda.Add(Buscado);
                        }
                    }
                    break;
            }

            return View(Singleton.Instance.pacientebusqueda);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(int id)
        {
            Singleton.Instance.id = id;
            Paciente ViewPaciente;
            ViewPaciente = Singleton.Instance.listSimulacion.ObtenerPos(id).Data.pacientePrioridad;
            return View(ViewPaciente);
           
        }

        public ActionResult Vacunacion(bool Estado)
        {
            Singleton.Instance.listSimulacion.ObtenerPos(Singleton.Instance.id).Data.pacientePrioridad.vacunado = Estado;
            Singleton.Instance.idSimulacro = 0;

            return Redirect("Details");

        }


        // GET: PacienteController/Create
        public ActionResult MenuReporte()//menu de reportes
        {
            return View();
        }

        public ActionResult Espera() //Página para desplegar los pacientes pendientes
        {
            hacerListaDeEspera();
            return View(Singleton.Instance.listaDeEspera);
        }
        public ActionResult Vacunados() //Página para desplegar los pacientes pendientes
        {
            return View(Singleton.Instance.listaPacientesVacunados);
        }
        public ActionResult Create()
        {
            return View();
        }
       
       
        public ActionResult Simulator(string municipio)
        {
            if (Singleton.Instance.idSimulacro == 1)
            {
                if (Singleton.Instance.listSimulacion.Cantidad > 0)
                {
                    for (int i = 0; i <= Singleton.Instance.listSimulacion.Cantidad; i++)
                    {
                        Singleton.Instance.listSimulacion.EliminarFinal();
                    }
                }
            }
            Singleton.Instance.idSimulacro = 1;
            if (municipio!=null)
            {
                int municipi = Convert.ToInt32(municipio);
                Singleton.Instance.Municipio = municipi;
                IniciarSimulacion(municipi);
            }
           
            return View(Singleton.Instance.listSimulacion);
        }

            // POST: PacienteController/Create
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newPaciente = new Models.Paciente
                {
                    dpi = Convert.ToInt32(collection["dpi"]),
                    nombre = collection["nombre"],
                    apellido = collection["apellido"],
                    departamento = Convert.ToInt32(collection["departamento"]),
                    municipio = Convert.ToInt32(collection["municipio"]),
                    edad = Convert.ToInt32(collection["edad"]),
                    grupo_prioridad = Convert.ToInt32(collection["grupo_prioridad"]),
                    vacunado = Convert.ToBoolean(collection["vacunado"])
                };                
                Singleton.Instance.PacienteList.Add(newPaciente);
                if (newPaciente.grupo_prioridad == -1)
                {
                    newPaciente.grupo_prioridad = Defprioridad(newPaciente.edad);
                }
                newPaciente.prioString = asignarPrio(newPaciente.grupo_prioridad);
                Municipios_Departamentos piv = new Municipios_Departamentos();
                string municipioP = piv.DevolverMunicipio(newPaciente.municipio); 
                string departamentoP = piv.DevolverDepartamento(newPaciente.departamento);
                newPaciente.municipioString = municipioP;
                newPaciente.departamentoString = departamentoP;
                // se instancia un objeto centro de vacunación y se instancia objeto paciente para los procesos del centro de vacunacion
                CentroVacunacion newCentro = new CentroVacunacion(); 
                Paciente nuevoPaciente = new Paciente(newPaciente.nombre,newPaciente.apellido,newPaciente.dpi,newPaciente.departamento,newPaciente.municipio,newPaciente.edad,newPaciente.grupo_prioridad, municipioP, departamentoP,newPaciente.prioString);
                //se igualaran variables para poder procesos próximos                                 
                int prioridad = newPaciente.grupo_prioridad;
                int municipioPivot = newPaciente.municipio;
                PacienteIndice nuevoPacienteIndice = new PacienteIndice(nuevoPaciente.nombre,nuevoPaciente.apellido,nuevoPaciente.dpi);
                newCentro.insertarPaciente(nuevoPaciente, nuevoPacienteIndice, prioridad);                
                int posicionEncontrada = 0;                
                bool encontrado = false;
                Singleton.Instance.listaPacientes.AgregarInicio(nuevoPaciente);
                //Lista de pacientes que servira para procesos de reporte
                Singleton.Instance.listaGeneralDePacientes.AgregarInicio(nuevoPaciente);
                //Validaciones para poder ingresar en la lista de pacientes
                if (Singleton.Instance.cantidadCentros == 0)
                {
                    Singleton.Instance.listaReferencia.AgregarPos(Singleton.Instance.posicion, municipioPivot);
                    Singleton.Instance.listaCentrosVacunacion.AgregarPos(Singleton.Instance.posicion, newCentro);
                    Singleton.Instance.posicion++;
                    Singleton.Instance.cantidadCentros++;
                }
                else
                {
                    for (int i = 0; i < Singleton.Instance.cantidadCentros; i++)
                    {
                        if (municipioPivot == Singleton.Instance.listaReferencia.DevolverValue(i))
                        {
                            encontrado = true;
                            posicionEncontrada = i;
                            i = Singleton.Instance.cantidadCentros;
                        }
                    }
                    if (encontrado == false)
                    {
                        Singleton.Instance.listaReferencia.AgregarPos(Singleton.Instance.posicion, municipioPivot);
                        Singleton.Instance.listaCentrosVacunacion.AgregarPos(Singleton.Instance.posicion, newCentro);
                        Singleton.Instance.posicion++;
                        Singleton.Instance.cantidadCentros++;
                    }
                    else
                    {                        
                        CentroVacunacion tempVacunacion = new CentroVacunacion();
                        tempVacunacion = Singleton.Instance.listaCentrosVacunacion.DevolverValue(posicionEncontrada);
                        tempVacunacion.insertarPaciente(nuevoPaciente, nuevoPacienteIndice, prioridad);
                        Singleton.Instance.listaCentrosVacunacion.Eliminarpos(posicionEncontrada);
                        Singleton.Instance.listaCentrosVacunacion.AgregarPos(posicionEncontrada, tempVacunacion);

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var editPaciente = Singleton.Instance.PacienteList.Find(x => x.dpi == id);
            Singleton.Instance.PacienteList.Remove(editPaciente);
            
            return View(editPaciente);
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                
                var newPaciente = new Models.Paciente
                {
                    dpi = Convert.ToInt32(collection["dpi"]),
                    nombre = collection["nombre"],
                    apellido = collection["apellido"],
                    departamento = Convert.ToInt32(collection["departamento"]),
                    municipio = Convert.ToInt32(collection["municipio"]),
                    edad = Convert.ToInt32(collection["edad"]),
                    grupo_prioridad = Convert.ToInt32(collection["grupo_prioridad"]),
                    vacunado = Convert.ToBoolean(collection["vacunado"])
                };
                Singleton.Instance.PacienteList.Add(newPaciente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Delete/5
        public ActionResult Delete(int id)
        {
            var deletePaciente = Singleton.Instance.PacienteList.Find(x => x.dpi == id);
            return View(deletePaciente);
        }

        // POST: PacienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var deletePaciente = Singleton.Instance.PacienteList.Find(x => x.dpi == id);
                Singleton.Instance.PacienteList.Remove(deletePaciente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

         
        public void IniciarSimulacion(int id)
        {            
            int muniPivot=id;
            bool encontradoI = false;
            int posicionEncontradaI = 0;
            for (int i = 0; i < Singleton.Instance.cantidadCentros; i++)
            {
                if (muniPivot == Singleton.Instance.listaReferencia.DevolverValue(i))
                {
                    encontradoI = true;
                    posicionEncontradaI = i;
                    i = Singleton.Instance.cantidadCentros;
                }
            }
            if (encontradoI == false)
            {
                //no hay datos
            }
            else if (encontradoI == true && Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.pacientesPrioridad < 3)
            {
                int valueCiclo = Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.pacientesPrioridad;
                for (int i = 0; i < valueCiclo; i++)
                {
                    //Singleton.Instance.listSimulacion.InsertarInicio(Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.priodadPaciente.pacPrioridad.ObtenerInicio());
                    Singleton.Instance.listSimulacion.AgregarFinal(Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.priodadPaciente.pacPrioridad.ObtenerPos(0).Data);
                    Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.ExtraerPrioridad();
                }
            }
            else if (encontradoI == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    Singleton.Instance.listSimulacion.AgregarFinal(Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.priodadPaciente.pacPrioridad.ObtenerPos(0).Data);
                    Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.ExtraerPrioridad();                     

                }
            }
        }  

        public int Defprioridad(int age)
        {
            if (age >= 70)
            {
                return 7;
            }
            else if (age>=60 && age < 70)
            {
                return 8;
            }
            else if(age>=50 && age < 60)
            {
                return 9;
            }
            else if (age>=40 && age<50)
            {
                return 16;
            }
            else
            {
                return 17;
            }
        }
        public string asignarPrio(int prioridad)
        {
            if(prioridad == 1)
            {
                return "1A";
            }
            else if (prioridad == 2)
            {
                return "1B";
            }
            else if (prioridad == 3)
            {
                return "1C";
            }
            else if (prioridad == 4)
            {
                return "1D";
            }
            else if (prioridad == 5)
            {
                return "1E";
            }
            else if (prioridad == 6)
            {
                return "1F";
            }
            else if (prioridad == 7)
            {
                return "2A";
            }
            else if (prioridad == 8)
            {
                return "2B";
            }
            else if (prioridad == 9)
            {
                return "2C";
            }
            else if (prioridad == 10)
            {
                return "2D";
            }
            else if (prioridad == 11)
            {
                return "2E";
            }
            else if (prioridad == 12)
            {
                return "3A";
            }
            else if (prioridad == 13)
            {
                return "3B";
            }
            else if (prioridad == 14)
            {
                return "3C";
            }
            else if (prioridad == 15)
            {
                return "3D";
            }
            else if (prioridad == 16)
            {
                return "4A";
            }
            else 
            {
                return "4B";
            }            
        }
        public void hacerListaDeEspera()
        {
            Singleton.Instance.listaDeEspera.EliminarTodo();
            for (int i = 0; i < Singleton.Instance.listaGeneralDePacientes.Cantidad; i++)
            {
                if (Singleton.Instance.listaGeneralDePacientes.ObtenerPos(i).Data.vacunado == false)
                {
                    Singleton.Instance.listaDeEspera.AgregarFinal(Singleton.Instance.listaGeneralDePacientes.ObtenerPos(i).Data);
                }
            }
        }
        public void porcentajeVacunados()
        {
            decimal total = (Singleton.Instance.listaPacientesVacunados.Cantidad * 100) / Singleton.Instance.listaGeneralDePacientes.Cantidad;
            Singleton.Instance.porcVac = decimal.Round(total, 2);            
        }

        public void hacerListaVacunados(int id)
        {
            int muniPivot = id;
            bool encontradoI = false;
            int posicionEncontradaI = 0;
            for (int i = 0; i < Singleton.Instance.cantidadCentros; i++)
            {
                if (muniPivot == Singleton.Instance.listaReferencia.DevolverValue(i))
                {
                    encontradoI = true;
                    posicionEncontradaI = i;
                    i = Singleton.Instance.cantidadCentros;
                }
            }
            for (int i = 0; i < Singleton.Instance.listSimulacion.Cantidad; i++)
            {
                if(Singleton.Instance.listSimulacion.ObtenerPos(i).Data.pacientePrioridad.vacunado == true)
                {
                    Singleton.Instance.listaPacientesVacunados.AgregarFinal(Singleton.Instance.listSimulacion.ObtenerPos(i).Data.pacientePrioridad);
                }
                else
                {
                    int PrioridadIndice = Singleton.Instance.listSimulacion.ObtenerPos(i).Data.prioridad;
                    Paciente pacIn = Singleton.Instance.listSimulacion.ObtenerPos(i).Data.pacientePrioridad;
                    Singleton.Instance.listaCentrosVacunacion.ObtenerPos(posicionEncontradaI).Data.ReinsertarPrioridad(PrioridadIndice,pacIn);
                }
            }
        }
        public ActionResult VerificarEstadoDeVacunacion() 
        {
            return Redirect("Index");            
        }

        public ActionResult FinalizarSimulacro()
        {
            hacerListaVacunados(Singleton.Instance.Municipio);
            return Redirect("Index");
        }
    }        
}
