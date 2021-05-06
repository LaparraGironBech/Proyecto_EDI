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
        Lista<PrioridadIndice> listSimulacion = new Lista<PrioridadIndice>();
        // GET: PacienteController
        public ActionResult Index()
        {
            return View(Singleton.Instance.PacienteList);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(int id)
        {
            var ViewJugadores = Singleton.Instance.PacienteList.Find(x => x.dpi == id);
            return View(ViewJugadores);
        }

        

        // GET: PacienteController/Create
        public ActionResult Create()
        {
            return View();
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
                Municipios_Departamentos piv = new Municipios_Departamentos();
                string municipioP = piv.DevolverMunicipio(newPaciente.municipio); 
                string departamentoP = piv.DevolverDepartamento(newPaciente.departamento);
                newPaciente.municipioString = municipioP;
                newPaciente.departamentoString = departamentoP;
                // se instancia un objeto centro de vacunación y se instancia objeto paciente para los procesos del centro de vacunacion
                CentroVacunacion newCentro = new CentroVacunacion(); 
                Paciente nuevoPaciente = new Paciente(newPaciente.nombre,newPaciente.apellido,newPaciente.dpi,newPaciente.departamento,newPaciente.municipio,newPaciente.edad,newPaciente.grupo_prioridad, municipioP, departamentoP);
                //se igualaran variables para poder procesos próximos                                 
                int prioridad = newPaciente.grupo_prioridad;
                int municipioPivot = newPaciente.municipio;
                PacienteIndice nuevoPacienteIndice = new PacienteIndice(nuevoPaciente.nombre,nuevoPaciente.apellido,nuevoPaciente.dpi);
                newCentro.insertarPaciente(nuevoPaciente, nuevoPacienteIndice, prioridad);                
                int posicionEncontrada = 0;                
                bool encontrado = false;
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
            if (Singleton.Instance.listaCentrosVacunacion.ObtenerPos(id).Data.pacientesPrioridad == 0)
            {
                //no hay datos
            }
            else if (Singleton.Instance.listaCentrosVacunacion.ObtenerPos(id).Data.pacientesPrioridad < 3)
            {
                for (int i = 0; i < Singleton.Instance.listaCentrosVacunacion.ObtenerPos(id).Data.pacientesPrioridad; i++)
                {
                    listSimulacion.InsertarInicio(Singleton.Instance.listaCentrosVacunacion.ObtenerPos(id).Data.priodadPaciente.pacPrioridad.ObtenerInicio());
                    
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    listSimulacion.InsertarInicio(Singleton.Instance.listaCentrosVacunacion.ObtenerPos(id).Data.priodadPaciente.pacPrioridad.ObtenerInicio());                    
                }
            }
        }  
        public ActionResult VerificarEstadoDeVacunacion() 
        {
            return Redirect("Index");            
        }
    }        
}
