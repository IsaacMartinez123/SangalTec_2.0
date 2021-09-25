using Microsoft.AspNetCore.Mvc;
using SangalTec.Bunsiness.Abstract;
using SangalTec.Models.Entities;
using SangalTec.WEB.Helpers;
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

        [NoDirectAccessAttribute]
        [HttpGet]
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
                        return Json(new { isValid = true, operacion = "crear" });
                    }

                    return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el producto" });
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "danger", error = "Error al crear la categoria" });
                }
                

            }

            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", categoria) });

        }

        [NoDirectAccessAttribute]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id != null)
            {
                try
                {
                    var producto = await _ICategoriaBunsiness.ObtenerCategoriaPorId(id);
                    if (producto != null)
                    {
                        return View(producto);

                    }
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });

                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }

            }
            return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
        }
    }
}
