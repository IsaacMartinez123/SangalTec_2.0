using Microsoft.AspNetCore.Mvc;
using SangalTec.Bunsiness.Abstract;
using SangalTec.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SangalTec.WEB.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaBunsiness _ICategoriaBunsiness;

        public CategoriasController(ICategoriaBunsiness CategoriaBunsiness)
        {
            _ICategoriaBunsiness = CategoriaBunsiness;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Lista de categoría";
            return View(await _ICategoriaBunsiness.ObtenerCategoria());
        }

        public IActionResult Crear()
        {
            ViewBag.Titulo = "Crear Categoría";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _ICategoriaBunsiness.Crear(categoria);
                    var guardar = await _ICategoriaBunsiness.GuardarCambios();

                    if (guardar)
                    {
                        TempData["Accion"] = "Guardar";
                        TempData["Mensaje"] = $"Se creo la categoria {categoria.Nombre}";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                

            }

            TempData["Accion"] = "Validacion";
            TempData["Mensaje"] = "Debe llenar los campos requeridos";
            return View(categoria);
        }
    }
}
