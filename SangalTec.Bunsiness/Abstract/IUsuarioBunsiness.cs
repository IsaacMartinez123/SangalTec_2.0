using SangalTec.Models.EntitiesUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Bunsiness.Abstract
{
    public interface IUsuarioBunsiness
    {
        Task<IEnumerable<Usuario>> ObtenerUsuarios();

        Task<Usuario> ObtenerUsuarioPorId(int? id);
    }

}
