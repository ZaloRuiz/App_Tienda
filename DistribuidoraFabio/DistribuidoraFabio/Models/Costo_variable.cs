using System;
using System.Collections.Generic;
using System.Text;

namespace DistribuidoraFabio.Models
{
    public class Costo_variable
    {
        public int id_cv { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_dia { get; set; }
        public string fecha_mes { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
    }
}
