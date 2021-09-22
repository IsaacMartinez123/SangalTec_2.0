using SangalTec.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Bunsiness.Abstract
{
    public interface IProductoBunsiness
    {
        Task<IEnumerable<Producto>> ObtenerProductos();

        void Crear(Producto producto);

        Task<Producto> ObtenerProductoPorId(int? id);

        Task<bool> GuardarCambios();
    }
}
