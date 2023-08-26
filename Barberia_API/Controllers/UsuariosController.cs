using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barberia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosModel _usuariosModel;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuariosModel usuariosModel, IConfiguration configuration)
        {
            _usuariosModel = usuariosModel;
            _configuration = configuration;
        }

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
    }
}
