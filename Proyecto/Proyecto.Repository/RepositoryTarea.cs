using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Data;

namespace Proyecto.Repository
{
    public interface IRepositoryTarea : IRepositoryBase<tarea>
    {
    }

    public class RepositoryTarea : RepositoryBase<tarea>, IRepositoryTarea
    {
        public RepositoryTarea() : base()
        { 
        }
    }

}
