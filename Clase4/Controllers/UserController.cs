// UserController.cs
using Clase4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;

namespace Clase4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<UserModel> _usuarios = new List<UserModel>();

        // Modificamos la lógica implementando la biblioteca de hash
        [HttpPost("CrearUsuario")]
        public IActionResult CrearUsuario([FromBody] UserModel usuario)
        {
            // Hashear la contraseña antes de guardarla
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            _usuarios.Add(usuario);
            // Guardar en la base de datos u otras operaciones necesarias.
            return Ok("Usuario creado exitosamente.");
        }

        // Modificamos la lógica también en obtener usuario
        // Al solicitar la información de un usuario, verifica también que la contraseña proporcionada
        // coincida con el hash
        [HttpGet("ObtenerUsuario")]
        public IActionResult ObtenerUsuario([FromQuery] string mail, [FromQuery] string contraseña)
        {
            var usuarioExistente = _usuarios.Find(u => u.Mail == mail);
            if (usuarioExistente != null)
            {
                // Verificar que la contraseña sea correcta
                if (BCrypt.Net.BCrypt.Verify(contraseña, usuarioExistente.Contraseña))
                {
                    // La contraseña es correcta, puedes devolver el usuario (sin la contraseña)
                    var usuarioSinContraseña = new UserModel
                    {
                        Nombre = usuarioExistente.Nombre,
                        Mail = usuarioExistente.Mail
                    };

                    return Ok(usuarioSinContraseña);
                }
            }

            return NotFound("Usuario no encontrado o contraseña incorrecta.");
        }

        [HttpPut("ActualizarUsuario")]
        public IActionResult ActualizarUsuario([FromBody] UserModel usuarioActualizado)
        {
            var usuarioExistente = _usuarios.Find(u => u.Mail == usuarioActualizado.Mail);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuarioActualizado.Nombre;
                return Ok("Usuario actualizado exitosamente.");
            }
            return NotFound("Usuario no encontrado.");
        }

        [HttpDelete("BorrarUsuario")]
        public IActionResult BorrarUsuario([FromQuery] string mail)
        {
            var usuarioExistente = _usuarios.Find(u => u.Mail == mail);
            if (usuarioExistente != null)
            {
                _usuarios.Remove(usuarioExistente);
                return Ok("Usuario eliminado exitosamente.");
            }
            return NotFound("Usuario no encontrado.");
        }
    }
}
