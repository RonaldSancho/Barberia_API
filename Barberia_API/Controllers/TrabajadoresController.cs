using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Barberia_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barberia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Se declara AllowAnonymous para poder ingresar a cada metodo sin tener que estar registrado
    [AllowAnonymous]
    public class TrabajadoresController : ControllerBase
    {

        //inyección de dependencias
        private readonly ITrabajadoresModel _trabajadoresModel;
        private readonly IConfiguration _configuration;

        public TrabajadoresController(ITrabajadoresModel trabajadoresModel, IConfiguration configuration)
        {
            _trabajadoresModel = trabajadoresModel;
            _configuration = configuration;
        }

        //Peticion HttpPost que permite ingresar trabajadores
        [HttpPost]
        [Route("RegistrarTrabajador")]
        public IActionResult RegistrarTrabajador(TrabajadoresEntities trabajador)
        {
            try
            {
                return Ok(_trabajadoresModel.RegistrarTrabajador(trabajador));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
