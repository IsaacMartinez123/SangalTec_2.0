using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Models.EntitiesUsers
{
    public class Usuario
    {
        [Display (Name = "Id")]
        public int UsuarioId { get; set; }

        public string Nombres { get; set; }

        public string Correo { get; set; }

        public string NumeroCelular { get; set; }

        public string Documento { get; set; }

        public bool Estado { get; set;  }

        //public int TipoDocumentoId { get; set; }

        //public virtual TipoDocumento TipoDocumento { get; set; }

        
    }
}
