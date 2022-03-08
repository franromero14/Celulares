using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Celulares.Models
{
    public class App
    {
        public int AppId { get; set; }

        public string  Nombre { get; set; }

        public virtual ICollection<Instalacion> Instalaciones { get; set; }
    }
}
