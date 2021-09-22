using Microsoft.AspNetCore.Mvc;
using SangalTec.Bunsiness.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SangalTec.WEB.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioBunsiness _usuarioBunsiness;
        

        public UsuariosController(IUsuarioBunsiness usuarioBunsiness)
        {
            _usuarioBunsiness = usuarioBunsiness;
        }

        public async Task <IActionResult> Index()
        {
            ViewBag.Titulo = "Lista de Usuarios";
            return View (await _usuarioBunsiness.ObtenerUsuarios());
        }
    }
}
