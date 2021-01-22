using System;
using System.Collections.Generic;
using System.Text;

namespace DistribuidoraFabio.Models
{
    public class Costo_fijo
    {
        public int id_costo_fijo { get; set; }
        public string nombre { get; set; }
        public decimal monto { get; set; }
        public string fecha_year { get; set; }
        public decimal DisplayMontoTotal => monto * 12;
    }
}
