using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SangalTec.Bunsiness.Abstract;
using SangalTec.Bunsiness.Dtos;
using SangalTec.DAL;
using SangalTec.Models.EntitiesUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangalTec.Bunsiness.Bunsiness
{
    public class UsuarioBunsiness : IUsuarioBunsiness
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioBunsiness(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UsuarioDto> ObtenerUsuarioDtoPorEmail(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario != null)
            {
                UsuarioDto usuarioDto = new()
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Estado = usuario.Estado,
                    NumeroCelular = usuario.PhoneNumber
                };
                return usuarioDto;
            }
            return null;
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerListaUsuarios()
        {
            List<UsuarioDto> listaUsuarioDtos = new();
            var usuarios = await _userManager.Users.ToListAsync();
            usuarios.ForEach(usuario =>
            {
                UsuarioDto usuarioDto = new()
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Estado = usuario.Estado,
                    NumeroCelular = usuario.PhoneNumber
                };
                listaUsuarioDtos.Add(usuarioDto);

            });
            return listaUsuarioDtos;
        }

        public async Task<Usuario> ObtenerUsuarioPorId(string? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> Crear(RegistrarUsuarioDto registrarUsuarioDto)
        {
            if (registrarUsuarioDto == null)
                throw new ArgumentNullException(nameof(registrarUsuarioDto));
            Usuario usuario = new()
            {
                UserName = registrarUsuarioDto.Email,
                Email = registrarUsuarioDto.Email,
                Estado = true,
                PhoneNumber = registrarUsuarioDto.NumeroCelular
            };
            var resultado = await _userManager.CreateAsync(usuario, registrarUsuarioDto.Password);
            if (resultado.Errors.Any())
                return "ErrorPassword";
            if (resultado.Succeeded)
                return usuario.Id;
            return null;

        }

       

        //public async Task<IEnumerable<Usuario>> Eliminar() 
        //{
            
        //}
    }
}
