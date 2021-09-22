using SangalTec.Models.EntitiesProvedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Bunsiness.Abstract
{
    public interface IProvedorBunsiness
    {
        Task<IEnumerable<Provedor>> ObtenerProvedores();
    }
}
