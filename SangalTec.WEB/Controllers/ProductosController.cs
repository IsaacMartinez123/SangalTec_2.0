using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SangalTec.Bunsiness.Abstract;
using SangalTec.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SangalTec.WEB.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoBunsiness _IProductoBunsiness;
        private readonly ICategoriaBunsiness _ICategoriaBunsiness;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductosController(IProductoBunsiness ProductoBunsiness, ICategoriaBunsiness ICategoriaBunsiness, IWebHostEnvironment hostingEnvironment)
        {
            _IProductoBunsiness = ProductoBunsiness;
            _hostingEnvironment = hostingEnvironment;
            _ICategoriaBunsiness = ICategoriaBunsiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Lista de productos";
            return View(await _IProductoBunsiness.ObtenerProductos());
        }

        public async Task<IActionResult> Crear()
        {
            ViewBag.Titulo = "Crear producto";
            ViewBag.Categoria = new SelectList(await _ICategoriaBunsiness.ObtenerCategoria(), "CategoriaId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string rutaPrincipal = _hostingEnvironment.WebRootPath;
                    var archivos = HttpContext.Request.Form.Files;

                   
                        //Nuevo artículo
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\productos");
                        var extesion = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extesion), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }

                        producto.UrlImagen = @"\imagenes\productos\" + nombreArchivo + extesion;

                        _IProductoBunsiness.Crear(producto);

                        var guardar = await _IProductoBunsiness.GuardarCambios();

                        if (guardar)
                        {
                            TempData["Accion"] = "Guardar";
                            TempData["Mensaje"] = $"Se creó el producto: {producto.Nombre}";
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
            ViewBag.Categoria = new SelectList(await _ICategoriaBunsiness.ObtenerCategoria(), "CategoriaId", "Nombre");
            return View(producto);
        }

        public async Task<IActionResult> Detalle(int? id)
        {
            if (id != null)
            {
                try
                {
                    var producto = await _IProductoBunsiness.ObtenerProductoPorId(id);
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
