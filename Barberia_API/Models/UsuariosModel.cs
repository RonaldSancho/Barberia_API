using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barberia_API.Models
{
    public class UsuariosModel : IUsuariosModel
    {
        private readonly IConfiguration _configuration;

        public UsuariosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //metodo que registra a los usuario
        public int RegistrarUsuario(UsuariosEntities usuario)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarUsuario",
                    new
                    {
                        usuario.Nombre,
                        usuario.Apellidos,
                        usuario.CorreoElectronico,
                        usuario.Contraseña,
                        usuario.Telefono
                    }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //metodo que valida si el correo ingresado existe
        public UsuariosEntities? ValidarCorreoElectronico(string CorreoElectronico)
        {
            using(var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("ValidarCorreoElectronico",
                    new { CorreoElectronico },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        //metodo que valida si el usuario existe
        public UsuariosEntities? ValidarUsuario(UsuariosEntities usuario)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("ValidarUsuario",
                    new { usuario.CorreoElectronico, usuario.Contraseña },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query<UsuariosEntities>("RecuperarContrasenna",
                    new { usuario.CorreoElectronico },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                if (resultado.CorreoElectronico == null) 
                    return null;
                else
                    return resultado;
            }
        }

        //Metodo que genera el JWT
        public string GenerarToken(string CorreoElectronico)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, CorreoElectronico)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
