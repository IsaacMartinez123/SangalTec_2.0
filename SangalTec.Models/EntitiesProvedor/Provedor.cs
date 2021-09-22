using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Models.EntitiesProvedor
{
    public class Provedor
    {
        public int ProvedorId { get; set; }

        public string NombreProvedor { get; set; }

        public string Direccion { get; set; }

        public string NumeroContacto { get; set; }

        public string CorreoProvedor { get; set; }
    }
}
