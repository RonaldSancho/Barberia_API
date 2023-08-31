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

        //Peticion HttpGet que permite consultar una lista de trabajadores
        [HttpGet]
        [Route("ConsultaTrabajadores")]
        public IActionResult ConsultaTrabajadores()
        {
            try
            {
                var resultado = _trabajadoresModel.ConsultaTrabajadores();
                if (resultado.Count > 0)
                    return Ok(resultado);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Peticion HttpGet que permite consultar a un trabajador por su ID
        [HttpGet]
        [Route("ConsultaTrabajador")]
        public IActionResult ConsultaTrabajador(int id)
        {
            try
            {
                var resultado = _trabajadoresModel.ConsultaTrabajador(id);

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

        //Peticion HttpPut que permite actualizar trabajadores
        [HttpPut]
        [Route("EditarTrabajador")]
        public IActionResult EditarTrabajador(TrabajadoresEntities trabajador)
        {
            try
            {
                _trabajadoresModel.EditarTrabajador(trabajador);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Peticion HttpDelete que permite Eliminar trabajadores
        [HttpDelete]
        [Route("EliminarTrabajador")]
        public IActionResult EliminarTrabajador(int id)
        {
            try
            {
                return Ok(_trabajadoresModel.EliminarTrabajador(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
