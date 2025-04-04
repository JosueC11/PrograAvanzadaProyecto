using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Data;
using Proyecto.Business;
using System.Text.RegularExpressions;
using Proyecto.Service;

namespace Proyecto.Controllers
{
    public class tareasController : Controller
    {
        private readonly TareaManager _manager = new TareaManager();
        private readonly MailService _mailService;

        public tareasController()
        {
            _mailService = new MailService();
        }
        // GET: tareas
        public ActionResult Index()
        {
            var tarea = _manager.GetAllTareas();
            return View(tarea);
        }

        // GET: tareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = (tarea)_manager.GetById(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // GET: tareas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,prioridad,fecha_creada,fecha_completada,exitosa")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _manager.Add(tarea);
                return RedirectToAction("Index");
            }

            return View(tarea);
        }

        //Hace tareas aleatorias, por el momento, hace 30
        public ActionResult TareasRandom()
        {
            Random random = new Random();

            var ultTarea = _manager.GetAllTareasUltima();   
            //Obtiene los numeros del string y lo asigna a una variable para hacer las tareas.
            int num = int.Parse(Regex.Match(ultTarea.nombre, @"\d+").Value);

            //Ciclo para crear las tareas y numeros
            for (int i = num + 1; i <= num + 29; i++)
            {
                var tarea = new tarea
                {
                    nombre = "Tarea-" + i,
                    prioridad = random.Next(1, 4), // Prioridad la escoge de uno a tres
                    fecha_creada = DateTime.Now // Pone la fecha de hoy                                       
                };

                _manager.Add(tarea);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CompletarTareas()
        {
            _manager.RealizaTarea();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TareasFallidas(int? id)
        {
            _manager.RealizaTareaCompleta(id);
            return RedirectToAction("Index");
        }

        public ActionResult Filtros(string expression)
        {
            var tarea = _manager.Filtro(expression);
            return Json(tarea, JsonRequestBehavior.AllowGet);
        }

        // GET: tareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Verifica que la categoria no sea null
            //Se hace explicito que se devuelva como una categoria y no un objeto
            var tarea = _manager.GetById(id) as tarea;

            if (tarea == null)
            {
                return HttpNotFound();
            }

            //Devuelve el bool del Save
            bool result = _manager.Save(tarea);
            if (!result)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,prioridad,fecha_creada,fecha_completada,exitosa")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _manager.Save(tarea);
                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        // GET: tareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = _manager.GetById(id) as tarea;
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tarea tarea = _manager.GetById(id) as tarea;
            _manager.Delete(id);
            _manager.Save(tarea);
            return RedirectToAction("Index");
        }

        public ActionResult SendFailedTasks()
        {
            bool sendingtasks = _mailService.SendMail();
            if (sendingtasks)
            {
                ViewBag.Message = "Email sent successfully.";
            }
            else
            {
                ViewBag.Message = "Failed to send email.";
            }
            return RedirectToAction("Index");
        }

    }
}
