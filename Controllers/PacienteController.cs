using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_EDI.Models.Data;

namespace Proyecto_EDI.Controllers
{
    public class PacienteController : Controller
    {
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
                    departamento = collection["departamento"],
                    municipio = collection["municipio"],
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

        // GET: PacienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var editPaciente = Singleton.Instance.PacienteList.Find(x => x.dpi == id);
            return View(editPaciente);
        }

        // POST: PacienteController/Edit/5
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
    }
}
