using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;


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

        //Peticion HttpPost que permite al usuario recuperar su contraseña
        [HttpPost]
        [Route("RecuperarContrasenna")]
        public IActionResult RecuperarContrasenna(UsuariosEntities usuario)
        {
            var resultado = _usuariosModel.RecuperarContrasenna(usuario);

            string EmailRemitente = _configuration.GetSection("EmailConfiguracion:EmailRemitente").Value;
            string Titulo = _configuration.GetSection("EmailConfiguracion:Titulo").Value;
            string Contraseña = _configuration.GetSection("EmailConfiguracion:Contraseña").Value;
            string Host = _configuration.GetSection("EmailConfiguracion:Host").Value;
            string Puerto = _configuration.GetSection("EmailConfiguracion:Puerto").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(EmailRemitente));
            email.To.Add(MailboxAddress.Parse(usuario.CorreoElectronico));
            email.Subject = Titulo;
            email.Body = new TextPart(TextFormat.Html)
            { Text = @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }
                            .container {
                                max-width: 600px;
                                margin: 20px auto;
                                padding: 20px;
                                background-color: #ffffff;
                                box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
                            }
                            h1 {
                                color: #333333;
                            }
                            p {
                                color: #666666;
                            }
                            .footer {
                                text-align: center;
                                margin-top: 20px;
                                padding-top: 20px;
                                border-top: 1px solid #dddddd;
                                color: #999999;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Recuperación de Contraseña</h1>
                            <p>Estimado usuario,</p>
                            <p>Hemos recibido una solicitud para recuperar la contraseña asociada a tu cuenta. A continuación, encontrarás tus detalles de inicio de sesión:</p>
                            <p><strong>Correo electrónico:</strong> " + resultado?.CorreoElectronico + @"</p>
                            <p><strong>Contraseña:</strong> " + resultado?.Contraseña + @"</p>
                            <p>Por razones de seguridad, te recomendamos cambiar tu contraseña después de iniciar sesión.</p>
                            <p>¡Gracias por confiar en nosotros!</p>
                            <div class='footer'>
                                <p>No responder a este correo.</p>
                            </div>
                        </div>
                    </body>
                    </html>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(Host, int.Parse(Puerto), true);
            smtp.Authenticate(EmailRemitente, Contraseña);
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
