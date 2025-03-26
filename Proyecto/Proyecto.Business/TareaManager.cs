using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Data;
using Proyecto.Repository;

namespace Proyecto.Business
{
    public class TareaManager
    {
        private readonly IRepositoryTarea _repositoryTarea;
        public TareaManager()
        {
            _repositoryTarea = new RepositoryTarea();
        }

        public IEnumerable<tarea> GetAllTareas()
        {
            //Order by es ascendente
            var tareas = _repositoryTarea.GetAll().ToList().OrderBy(n => n.prioridad);
            return tareas;
        }

        //Devuelve la ultima tarea
        public tarea GetAllTareasUltima()
        {
            //Order by es ascendente
            var tarea = _repositoryTarea.GetAll().LastOrDefault();
            return tarea;
        }

        public void Add(tarea category)
        {
            _repositoryTarea.Add(category);
        }

        public object GetById(int? id)
        {
            var tareas = _repositoryTarea.GetById((int)id);
            return tareas;
        }

        public bool Save(tarea tarea)
        {
            _repositoryTarea.Update(tarea);
            return true;
        }

        public void Delete(int? id)
        {
            _repositoryTarea.Delete((int)id);
        }

        public void RealizaTarea()
        {
            Random random = new Random();
            int num = random.Next(1, 3);
            //Agarra la lista de tareas donde exitosa sea null, osea, que no se ha completado la tarea
            var tarea = GetAllTareas().Where(n => n.exitosa == null).FirstOrDefault();
            //Valida que no este vacio
            if (tarea == null)
            {
                return;
            }
            if (num == 1)
            {
                tarea.exitosa = true;
                _repositoryTarea.Update(tarea);
            }
            else
            {
                tarea.exitosa = false;
                _repositoryTarea.Update(tarea);
            }
        }

        //Este metodo actualiza siempre a que sea true
        public void RealizaTareaCompleta(int? id)
        {
            var tarea = _repositoryTarea.GetById((int)id);
            tarea.exitosa = true;
            _repositoryTarea.Update(tarea);
        }

        public IEnumerable <tarea> Filtro(string grupo = "all")
        {
            if (grupo == "all")
                return GetAllTareas();
            if (grupo == "group1")
                return GetAllTareas().Where(n =>n.exitosa == false);
            if (grupo == "group2")
                return GetAllTareas().Where(n => n.exitosa == true);
            return GetAllTareas();
        }

    }
}
