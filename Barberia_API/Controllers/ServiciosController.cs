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
    public class ServiciosController : ControllerBase
    {
        //inyección de dependencias
        private readonly IServiciosModel _serviciosModel;
        private readonly IConfiguration _configuration;

        public ServiciosController(IServiciosModel serviciosModel, IConfiguration configuration)
        {
            _serviciosModel = serviciosModel;
            _configuration = configuration;
        }

        //Peticion HttpPost que permite ingresar servicios
        [HttpPost]
        [Route("RegistrarServicio")]
        public IActionResult RegistrarServicio(ServiciosEntities servicio)
        {
            try
            {
                return Ok(_serviciosModel.RegistrarServicio(servicio));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
