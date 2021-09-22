using Microsoft.EntityFrameworkCore;
using SangalTec.Bunsiness.Abstract;
using SangalTec.DAL;
using SangalTec.Models.EntitiesProvedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Bunsiness.Bunsiness
{
    public class ProvedorBunsiness : IProvedorBunsiness
    {
        private readonly SangalDbContext _context;

        public ProvedorBunsiness(SangalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provedor>> ObtenerProvedores()
        {
            return await _context.Provedores.Include(x => x.ProvedorId).ToListAsync();
        }
    }
}
