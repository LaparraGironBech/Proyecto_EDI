using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_EDI.Models.Data;

namespace Proyecto_EDI.Controllers
{
    public class PersonasController : Controller
    {
        // GET: PersonasController
        public ActionResult Index()
        {
            return View(Singleton.Instance.PersonasList);
        }

        // GET: PersonasController/Details/5
        public ActionResult Details(int id)
        {
            var ViewPersonas = Singleton.Instance.PersonasList.Find(x => x.Id == id);
            return View(ViewPersonas);
        }

        // GET: PersonasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newPersonas = new Models.Personas
                {
                    Id = Convert.ToInt32(collection["Id"]),
                    Name = collection["Name"],
                    Lastname = collection["Lastname"],
                    Cui = Convert.ToInt32(collection["Cui"]),
                    Departamento = collection["Departamento"],
                    Municipio = collection["Municipio"],
                    Trabajo = collection["Trabajo"]
                };
                Singleton.Instance.PersonasList.Add(newPersonas);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonasController/Edit/5
        public ActionResult Edit(int id)
        {
            var editPersonas = Singleton.Instance.PersonasList.Find(x => x.Id == id);
            return View(editPersonas);
            
        }

        // POST: PersonasController/Edit/5
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

        // GET: PersonasController/Delete/5
        public ActionResult Delete(int id)
        {
            var deletePersonas = Singleton.Instance.PersonasList.Find(x => x.Id == id);

            return View(deletePersonas);
        }

        // POST: PersonasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var deletePersonas = Singleton.Instance.PersonasList.Find(x => x.Id == id);
                Singleton.Instance.PersonasList.Remove(deletePersonas);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
