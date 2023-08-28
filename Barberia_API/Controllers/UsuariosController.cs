using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barberia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Se declara AllowAnonymous para poder ingresar a cada metodo sin tener que estar registrado
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        //inyección de dependencias
        private readonly IUsuariosModel _usuariosModel;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuariosModel usuariosModel, IConfiguration configuration)
        {
            _usuariosModel = usuariosModel;
            _configuration = configuration;
        }

        //Peticion HttpPost que permite ingresar usuarios
        [HttpPost]
        [Route("RegistrarUsuario")]
        public IActionResult RegistrarUsuario(UsuariosEntities usuario) 
        {
            try
            {
                return Ok(_usuariosModel.RegistrarUsuario(usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Peticion HttpGet que permite validar si el correo existe
        [HttpGet]
        [Route("ValidarCorreoElectronico")]
        public IActionResult ValidarCorreoElectronico(string q)
        {
            try
            {
                var resultado = _usuariosModel.ValidarCorreoElectronico(q);
                if (resultado != null)
                    return Ok(resultado);
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Peticion HttpPost que permite validar si el usuario existe
        [HttpPost]
        [Route("ValidarUsuario")]
        public IActionResult ValidarUsuario(UsuariosEntities usuario)
        {
            var resultado = _usuariosModel.ValidarUsuario(usuario);

            if (resultado != null)
            {
                //se genera un token para identificar al usuario
                resultado.Token = _usuariosModel.GenerarToken(resultado.CorreoElectronico);
                return Ok(resultado);
            }
            else
                return NotFound();
        }
    }
}
