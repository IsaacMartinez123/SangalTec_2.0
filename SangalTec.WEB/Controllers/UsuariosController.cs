using Microsoft.AspNetCore.Mvc;
using SangalTec.Bunsiness.Abstract;
using SangalTec.Bunsiness.Dtos;
using SangalTec.Models.EntitiesUsers;
using SangalTec.WEB.Helpers;
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

        public async Task<IActionResult> Index()
        {
            return View(await _usuarioBunsiness.ObtenerListaUsuarios());
        }

        [NoDirectAccessAttribute]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear(RegistrarUsuarioDto registrarUsuario)
        {
            if (ModelState.IsValid)
            {
                //comprobar su existe el usuario con ese correo

                var email = await _usuarioBunsiness.ObtenerUsuarioDtoPorEmail(registrarUsuario.Email);
                if (email == null)
                {
                    try
                    {
                        var usuarioId = await _usuarioBunsiness.Crear(registrarUsuario);

                        if (usuarioId == null)
                            return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });

                        if (usuarioId.Equals("ErrorPassword"))
                            return Json(new { isValid = false, tipoError = "danger", error = "Error de password" });

                        return Json(new { isValid = true, operacion = "crear" });
                    }
                    catch (Exception)
                    {

                        return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });
                    }
                }

            }

            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", registrarUsuario) });
        }



        [NoDirectAccessAttribute]
        public async Task<IActionResult> Detalle(string? id)
        {
            if (id != null)
            {
                try
                {
                    var usuario = await _usuarioBunsiness.ObtenerUsuarioPorId(id);
                    if (usuario != null)
                    {
                        return View(usuario);

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
