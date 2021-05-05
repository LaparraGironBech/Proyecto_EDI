using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_EDI.Models.Data;
using Proyecto_EDI.Models;
using Proyecto_EDI.Controllers;

namespace Proyecto_EDI.Controllers
{
    public class CentroVacunacionController : Controller
    {
        // GET: CentroVacunacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CentroVacunacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CentroVacunacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentroVacunacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                //if (Singleton.Instance.listaCentrosVacunacion.ObtenerPos(0).Data.totalPacientes<3) { }
                //Paciente newPac = Singleton.Instance.listaCentrosVacunacion.ObtenerPos(0).Data.priodadPaciente.pacPrioridad.PrimerNodo.Data.pacientePrioridad;
                 
                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CentroVacunacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CentroVacunacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CentroVacunacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: CentroVacunacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
